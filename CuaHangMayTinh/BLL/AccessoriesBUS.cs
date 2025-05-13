using System;
using System.Data;
using CuaHangMayTinh.DAL;
using CuaHangMayTinh.DTO.Common;

namespace CuaHangMayTinh.BLL
{
    public class AccessoriesBUS
    {
        private AccessoriesDAO accessoriesDAO;
        private ProductDAO productDAO;

        public AccessoriesBUS()
        {
            accessoriesDAO = new AccessoriesDAO();
            productDAO = new ProductDAO();
        }

        public int InsertAccessories(string accessoriesName, string overview, int supplierId, string productName, decimal price, int stockQuantity)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(accessoriesName))
                    throw new ArgumentException("Tên thiết bị không được để trống");
                if (string.IsNullOrWhiteSpace(overview))
                    throw new ArgumentException("Mô tả không được để trống");
                if (supplierId <= 0)
                    throw new ArgumentException("Nhà cung cấp không hợp lệ");
                if (string.IsNullOrWhiteSpace(productName))
                    throw new ArgumentException("Tên sản phẩm không được để trống");
                if (price <= 0)
                    throw new ArgumentException("Giá phải lớn hơn 0");
                if (stockQuantity < 0)
                    throw new ArgumentException("Số lượng tồn kho không được âm");

                int productId = accessoriesDAO.Insert(accessoriesName, overview, supplierId, productName, price, stockQuantity);
                if (productId <= 0)
                    throw new Exception("Không thể thêm thiết bị");
                return productId;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm thiết bị: " + ex.Message, ex);
            }
        }

        public DataTable GetAllAccessories()
        {
            return accessoriesDAO.GetAll();
        }

        public int DeleteAccessories(int productId)
        {
            return accessoriesDAO.Delete(productId);
        }

        public int UpdateAccessories(int productId, string accessoriesName, string overview, int supplierId, string productName, decimal price, int stockQuantity)
        {
            try
            {
                // Validate input (reuse validation from InsertAccessories)
                if (string.IsNullOrWhiteSpace(accessoriesName))
                    throw new ArgumentException("Tên thiết bị không được để trống");
                if (string.IsNullOrWhiteSpace(overview))
                    throw new ArgumentException("Mô tả không được để trống");
                if (supplierId <= 0)
                    throw new ArgumentException("Nhà cung cấp không hợp lệ");
                if (string.IsNullOrWhiteSpace(productName))
                    throw new ArgumentException("Tên sản phẩm không được để trống");
                if (price <= 0)
                    throw new ArgumentException("Giá phải lớn hơn 0");
                if (stockQuantity < 0)
                    throw new ArgumentException("Số lượng tồn kho không được âm");

                // Call the DAO's update method
                return accessoriesDAO.Update(productId, accessoriesName, overview, supplierId, productName, price, stockQuantity);
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật thiết bị: " + ex.Message, ex);
            }
        }
    }
} 