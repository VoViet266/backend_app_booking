# Frontend Integration Flow - Booking System (Đăng ký khám)

Tài liệu này hướng dẫn cách FE (Frontend) tích hợp với các API để thực hiện quy trình đặt lịch khám bệnh.

## 1. Các API chính

| Chức năng | Method | Endpoint | Auth |
| :--- | :--- | :--- | :--- |
| Danh sách chuyên khoa | `GET` | `/api/chuyenkhoa/laydanhsach-chuyenkhoa` | No |
| Bác sĩ theo chuyên khoa | `GET` | `/api/bacsi/laybacsi_khoa/{mack}` | No |
| Danh sách hồ sơ bệnh nhân | `GET` | `/api/user/ho-so` | **Yes** |
| Thêm hồ sơ mới (người thân) | `POST` | `/api/user/ho-so` | **Yes** |
| Đặt lịch khám | `POST` | `/api/dangky` | Optional |
| Lịch sử đặt lịch | `GET` | `/api/dangky/cua-toi` | **Yes** |
| Chi tiết lịch hẹn | `GET` | `/api/dangky/{maDk}` | Optional |
| Hủy lịch hẹn | `PUT` | `/api/dangky/{maDk}/huy` | Optional |

---

## 2. Quy trình đặt lịch (Booking Flow)

### Bước 1: Chọn Chuyên khoa
- Gọi API `/api/chuyenkhoa/laydanhsach-chuyenkhoa`.
- Hiển thị danh sách `TenCk` và lấy `MaCk` khi người dùng chọn.

### Bước 2: Chọn Bác sĩ (Tùy chọn)
- Sau khi có `MaCk`, gọi API `/api/bacsi/laybacsi_khoa/{maCk}`.
- Hiển thị danh sách bác sĩ thuộc khoa đó.
- Nếu người chọn bác sĩ, lấy `Mabs` (mã nhân viên/id bác sĩ). Nếu không chọn, gửi `null` hoặc `""` (backend sẽ tự phân bổ).

### Bước 3: Thông tin Bệnh nhân (Patient Profile)
Có 2 trường hợp:

**A. User chưa đăng nhập (Guest):**
- Hiển thị form nhập: `HoTen`, `Ngaysinh`, `Sdt`, `Cmnd`, `Diachi`, `Mathe` (BHYT).

**B. User đã đăng nhập:**
- Gọi API `/api/user/ho-so` để hiển thị danh sách hồ sơ (bản thân + người thân).
- Nếu người dùng chọn hồ sơ sẵn có: Lấy thông tin từ hồ sơ đó điền vào form.
- Nếu muốn đặt cho người mới: Hiển thị form nhập và có option "Lưu hồ sơ này vào tài khoản" (gọi thêm `POST /api/user/ho-so`).

### Bước 4: Chọn ngày và khung giờ
- Chọn `Ngay` (format: `YYYY-MM-DD`).
- Chọn `TimeSlot` (format ISO: `YYYY-MM-DDTHH:mm:ssZ`).

### Bước 5: Gửi yêu cầu đặt lịch
Gọi `POST /api/dangky` với body `DatLichKhamRequest`:

```json
{
  "hoTen": "Nguyễn Văn A",
  "ngaysinh": "1990-01-01",
  "sdt": "0987654321",
  "cmnd": "123456789",
  "diachi": "Hà Nội",
  "mathe": "", 
  "maCk": "09",
  "mabs": "NV001", 
  "ngay": "2026-03-20",
  "timeSlot": "2026-03-20T08:30:00Z",
  "ghiChu": "Đau bụng kéo dài"
}
```

---

## 3. Quy trình quản lý lịch (Management Flow)

### Xem lịch sử (Dành cho User đã đăng nhập)
1. Gọi `GET /api/dangky/cua-toi` (kèm Bearer Token).
2. Hiển thị danh sách với các trạng thái: `Chờ xác nhận (0)`, `Đã xác nhận (1)`, `Đã hủy (2)`, `Hoàn thành (3)`.

### Xem chi tiết & Hủy lịch
1. Nhấn vào một lịch hẹn để gọi `GET /api/dangky/{maDk}`.
2. Nếu trạng thái là `0` hoặc `1` và chưa quá ngày khám, hiển thị nút "Hủy lịch".
3. Khi hủy, gọi `PUT /api/dangky/{maDk}/huy` với body:
   ```json
   { "lyDo": "Bận việc đột xuất" }
   ```

---

## 4. Lưu ý quan trọng cho FE
- **Case Sensitivity**: Đảm bảo field name gửi lên đúng casing (ví dụ: `hoTen` thay vì `hoten`) trừ khi Backend đã bật `PropertyNameCaseInsensitive`.
- **Date Format**: Luôn dùng `YYYY-MM-DD` cho `Ngay` và `ISO 8601` cho `TimeSlot`.
- **Validation**: Kiểm tra SĐT (10 số), CMND trước khi gửi để giảm tải cho server.
- **Trạng thái**: Map `TrangThai` (int) sang text trên UI để người dùng dễ hiểu.
