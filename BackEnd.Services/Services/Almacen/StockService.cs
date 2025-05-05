using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Almacen;
using BackEnd.Services.Services.Contable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEnd.Services.Services.Almacen
{
    public interface IStockService : IService<Stock, int>
    {
        IEnumerable<Stock> UpdateFromMovStock(List<Stock> entities);
        IEnumerable<Stock> UpdateFromMovStock();
        Stock Ajustar(FormAjusteStock form);
    }
    public class StockService : ServiceBase<Stock,int>,IStockService
    {

        public StockService(UnitOfWorkGestionDb UnitOfWork) : base(UnitOfWork)
        {
        
        }

        public Stock Ajustar(FormAjusteStock form)
        {
            //Primero Consultar stock actual
            Stock currentStock = this.GetOne(form.Stock.Id);
            decimal cantidadAjustar = currentStock.Cantidad - form.Cantidad;
            int tipoMov = 1; //Ingreso
            if (cantidadAjustar > 0) 
            {
                tipoMov = 2; //Egreso
            }
            var movStockRepository = new RepositoryBase<MovStock, int>(UnitOfWork);
            MovStock newMovStock = new MovStock();
            newMovStock.IdArticulo = form.Stock.IdArticulo;
            newMovStock.IdDeposito = form.Stock.IdDeposito;
            newMovStock.IdLote = form.Stock.IdLote;
            newMovStock.Concepto = form.Concepto;
            newMovStock.Fecha = form.Fecha;
            newMovStock.Tipo = tipoMov;
            newMovStock.Cantidad = Math.Abs(cantidadAjustar);
            movStockRepository.Add(newMovStock);
            //Actualizar Stock
            currentStock.Cantidad = form.Cantidad;
            this.Update(currentStock.Id, currentStock);
            this.UnitOfWork.Commit();
            return currentStock;
        }

        public override IEnumerable<Stock> GetAll()
        {
            return this._Repository.GetAll().Include("Articulo").Include("Deposito");
        }
        public IEnumerable<Stock>  UpdateFromMovStock(List<Stock> entities)
        {
            var movStockRepository = new RepositoryBase<MovStock, int>(UnitOfWork);
            // 1. Obtener todos los IDs necesarios para una sola consulta
            var articuloIds = entities.Select(e => e.IdArticulo).Distinct().ToList();
            var depositoIds = entities.Select(e => e.IdDeposito).Distinct().ToList();
            var loteIds = entities.Select(e => e.IdLote).Distinct().ToList();
            var serieIds = entities.Select(e => e.IdSerie).Distinct().ToList();

            // 2. Obtener todos los movimientos relevantes en una sola consulta agrupada
            var movimientosAgrupados = movStockRepository.GetAll()
                .Where(m => articuloIds.Contains(m.IdArticulo) &&
                           depositoIds.Contains(m.IdDeposito) &&
                           loteIds.Contains(m.IdLote) &&
                           serieIds.Contains(m.IdSerie))
                .AsEnumerable() // Cambia a LINQ to Objects
                .GroupBy(m => new { m.IdArticulo, m.IdDeposito, m.IdLote, m.IdSerie })
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(m => m.Tipo == 1 ? m.Cantidad : -m.Cantidad)
                );

            // 3. Obtener stocks existentes en una sola consulta
            var existingStocks = _Repository.GetAll()
                .Where(s => articuloIds.Contains(s.IdArticulo) &&
                           depositoIds.Contains(s.IdDeposito) &&
                           loteIds.Contains(s.IdLote) &&
                           serieIds.Contains(s.IdSerie))
                .ToList();

            // 4. Procesar cada entidad
            foreach (var entity in entities)
            {
                var key = new { entity.IdArticulo, entity.IdDeposito, entity.IdLote, entity.IdSerie };

                // Calcular cantidad
                entity.Cantidad = movimientosAgrupados.TryGetValue(key, out var cantidad) ? cantidad : 0;

                // Buscar stock existente
                var existingStock = existingStocks.FirstOrDefault(s =>
                    s.IdArticulo == entity.IdArticulo &&
                    s.IdDeposito == entity.IdDeposito &&
                    s.IdLote == entity.IdLote &&
                    s.IdSerie == entity.IdSerie);

                if (existingStock == null)
                {
                    _Repository.Add(entity);
                }
                else
                {
                    // Actualizar solo la cantidad para minimizar transferencia de datos
                    existingStock.Cantidad = entity.Cantidad;
                    _Repository.Update(existingStock.Id, existingStock);
                }
            }
            // 5. Guardar cambios una sola vez al final
            this.UnitOfWork.Commit();
            var result = this.GetAll().Where(s => articuloIds.Contains(s.IdArticulo) &&
                           depositoIds.Contains(s.IdDeposito) &&
                           loteIds.Contains(s.IdLote) &&
                           serieIds.Contains(s.IdSerie))
                .ToList();
            return result;
        }

        public IEnumerable<Stock> UpdateFromMovStock()
        {
            var articuloRepository = new RepositoryBase<Articulo, Guid>(UnitOfWork);
            var depositoRepository = new RepositoryBase<Deposito, string>(UnitOfWork);
            var loteRepository = new RepositoryBase<Lote, string>(UnitOfWork);
            var serieRepository = new RepositoryBase<Serie, string>(UnitOfWork);

            // Obtener todas las combinaciones posibles
            var articulos = articuloRepository.GetAll().Where(w => !w.EsServicio).Select(a => a.Id).ToList();
            var depositos = depositoRepository.GetAll().Select(d => d.Id).ToList();
            var lotes = loteRepository.GetAll().Select(l => l.Id).ToList();
            var series = serieRepository.GetAll().Select(s => s.Id).ToList();

            var allStocks = new List<Stock>();
            var batchSize = 100; // Procesar por lotes para mejor performance
            var currentBatch = new List<Stock>();

            foreach (var articuloId in articulos)
            {
                foreach (var depositoId in depositos)
                {
                    // Si no hay lotes, iteramos una vez con IdLote = null
                    var lotesToProcess = lotes.Any() ? lotes : new List<int> { 0 }; // 0 = "sin lote"
                    

                    foreach (var loteId in lotesToProcess)
                    {
                        // Si no hay series, iteramos una vez con IdSerie = null
                        var seriesToProcess = series.Any() ? series : new List<int> { 0 }; // 0 = "sin serie"

                        foreach (var serieId in seriesToProcess)
                        {
                            var stock = new Stock
                            {
                                IdArticulo = articuloId.ToString(),
                                IdDeposito = depositoId,
                                IdLote = loteId, // Puede ser null si no hay lotes
                                IdSerie = serieId, // Puede ser null si no hay series
                                Cantidad = 0 // Valor temporal
                            };

                            currentBatch.Add(stock);

                            // Procesar por lotes
                            if (currentBatch.Count >= batchSize)
                            {
                                UpdateFromMovStock(currentBatch);
                                allStocks.AddRange(currentBatch);
                                currentBatch.Clear();
                            }
                        }
                    }
                }
            }

            // Procesar el último lote si queda pendiente
            if (currentBatch.Any())
            {
                UpdateFromMovStock(currentBatch);
                allStocks.AddRange(currentBatch);
            }

            return this.GetAll();
        }

        public override ValidationResults ValidateUpdate(Stock entity)
        {
            var result =  base.ValidateUpdate(entity);            
            return result;
        }
        
    }
}
