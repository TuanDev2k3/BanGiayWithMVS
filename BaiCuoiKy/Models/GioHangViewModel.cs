namespace BaiCuoiKy.Models
{
	public class GioHangViewModel
	{
		//Luu thong tin SP trong Gio Hang
		public IEnumerable<GioHang> DsGioHang { get; set; }
		// Luu tru tong tien gio hang
		//public double TotalPrice { get; set; }
		public HoaDon HoaDon { get; set; }
	}
}
