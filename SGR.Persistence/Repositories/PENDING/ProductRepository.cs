using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using SGR.Application.Contracts.Repository;
using SGR.Application.Dtos.Restaurant_and_Products.Products;
using SGR.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Persistence.Repositories.PENDING
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(string connectionString, ILogger<ProductRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public async Task<OperationResult> AddAsync(CreateProductDTO dto)
        {
            var result = new OperationResult();
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                using var command = new MySqlCommand("sp_CreateProduct", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Name", dto.Name);
                command.Parameters.AddWithValue("@Description", dto.Description);
                command.Parameters.AddWithValue("@Price", dto.Price);
                command.Parameters.AddWithValue("@MenuCategoryId", dto.MenuCategoryId);
                command.Parameters.AddWithValue("@CreatedAt", dto.CreatedAt);
                command.Parameters.AddWithValue("@CreatedBy", dto.CreatedBy);

                await connection.OpenAsync();
                var affected = await command.ExecuteNonQueryAsync();

                result.IsSuccess = affected > 0;
                result.Message = affected > 0 ? "Producto creado" : "No se pudo crear el producto";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creando producto");
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<OperationResult> UpdateAsync(ModifyProductDTO dto)
        {
            var result = new OperationResult();
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                using var command = new MySqlCommand("sp_UpdateProduct", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", dto.IdProduct);
                command.Parameters.AddWithValue("@Name", dto.Name);
                command.Parameters.AddWithValue("@Description", dto.Description);
                command.Parameters.AddWithValue("@Price", dto.Price);
                command.Parameters.AddWithValue("@UpdatedBy", dto.UpdatedBy);

                await connection.OpenAsync();
                var affected = await command.ExecuteNonQueryAsync();

                result.IsSuccess = affected > 0;
                result.Message = affected > 0 ? "Producto modificado" : "No se pudo modificar el producto";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error modificando producto");
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<OperationResult> DeleteAsync(DisableProductDTO dto)
        {
            var result = new OperationResult();
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                using var command = new MySqlCommand("sp_DisableProduct", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", dto.IdProduct);
                command.Parameters.AddWithValue("@DeletedBy", dto.DeletedBy);

                await connection.OpenAsync();
                var affected = await command.ExecuteNonQueryAsync();

                result.IsSuccess = affected > 0;
                result.Message = affected > 0 ? "Producto deshabilitado" : "No se pudo deshabilitar el producto";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deshabilitando producto");
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public Task<OperationResult> GetAllAsync() => throw new NotImplementedException();
        public Task<OperationResult> GetByIdAsync(int id) => throw new NotImplementedException();
    }
}
