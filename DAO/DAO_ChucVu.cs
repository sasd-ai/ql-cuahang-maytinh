using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAO
{
    public class DAO_ChucVu
    {
        private QL_CuaHangDataContext qlch = new QL_CuaHangDataContext();
        public DAO_ChucVu() { }

        //Lấy danh sách chức vụ
        public List<chucvu> GetData()
        {
            return qlch.chucvus.Select(cv => cv).ToList<chucvu>();
        }
        public chucvu FindByID(string maCV)
        {
            return qlch.chucvus.Where(cv=>cv.MaCV == maCV).FirstOrDefault();
        }

        public bool Delete(chucvu cv)
        {
            try
            {
                qlch.chucvus.DeleteOnSubmit(cv);
                qlch.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        //Load bảng phân quyền theo mã chức vụ
        public List<BangQuyenManHinh> GetPhanQuyenByMaCV(string maCV)
        {
            var quyen = from mh in qlch.ManHinhs
                        join pq in qlch.PhanQuyens.Where(p => p.MaCV == maCV) on mh.MaManHinh equals pq.MaManHinh into pqGroup
                        from pq in pqGroup.DefaultIfEmpty()
                        select new
                        {
                            MaCV = pq == null ? maCV : pq.MaCV,
                            MaMH = mh.MaManHinh,
                            TenManHinh = mh.TenManHinh,
                            CoQuyen = pq == null ? (bool?)null : pq.CoQuyen
                        };

            // Chuyển đổi dữ liệu sau khi truy vấn
            var result = quyen.AsEnumerable()
                              .Select(x => new BangQuyenManHinh
                              {
                                  MaCV = x.MaCV,
                                  MaMH = x.MaMH,
                                  TenManHinh = x.TenManHinh,
                                  CoQuyen = x.CoQuyen ?? false // Chuyển đổi từ bool? sang bool
                              })
                              .ToList();

            // Trả về kết quả truy vấn dưới dạng danh sách
            return result;
        }
        //Tìm quyền theo chức vụ
        public PhanQuyen FindByID(string maCV,string maManHinh)
        {
            return qlch.PhanQuyens.Where(pq=>pq.MaCV==maCV && pq.MaManHinh==maManHinh).FirstOrDefault();
        }

        //Update quyền cho nhóm người dùng
        public bool UpdatePhanQuyen(string maCV,string maManHinh,bool coQuyen)
        {
            try
            {
                PhanQuyen pq = FindByID(maCV, maManHinh);
                if(pq!=null)
                {
                    //Gán lại quyền cho nhóm
                    pq.CoQuyen = coQuyen;
                    qlch.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        //Insert quyền cho nhóm người dùng
        public bool InsertQuyen(string maCV, string maManHinh)
        {
            try
            {
                PhanQuyen pq = new PhanQuyen()
                {
                    MaCV = maCV,
                    MaManHinh = maManHinh,
                    CoQuyen = true
                };
                qlch.PhanQuyens.InsertOnSubmit(pq);
                qlch.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());  
                return false;
            }
        }
    }
}
