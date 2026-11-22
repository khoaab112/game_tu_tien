# THƯ MỤC TẠM (TEMP) - TÀI LIỆU & CÔNG CỤ ĐỒ HỌA TOP-DOWN PIXEL ART

Thư mục này chứa các đặc tả kỹ thuật, bộ prompt mẫu và công cụ hỗ trợ xử lý đồ họa cho dự án Game Tu Tiên theo định hướng **Top-Down 2D Hi-Res Pixel Art (128x128)**.

---

### Danh sách các file trong thư mục:

1. **[PIXEL_ART_TOPDOWN_GUIDE.md](PIXEL_ART_TOPDOWN_GUIDE.md)**
   * **Đặc tả kỹ thuật:** Chuẩn kích thước `128x128 px`, điểm neo Pivot `(0.5, 0.12)` tại chân nhân vật.
   * **Bảng màu (Palette):** Mã màu Hex cho y phục đen, viền ngọc bích và dải Chân Khí phát sáng. Bảng màu chuyển đổi 5 hệ ngũ hành (Hỏa đỏ, Kim vàng, Thủy lam, Mộc ngọc bích, Ma đạo tím).
   * **Bộ Prompts AI:** Mẫu câu lệnh chi tiết cho Meowa.ai / Stable Diffusion / Midjourney để tạo các chuyển động Walk Down, Walk Up, Walk Side, Attack.
   * **Quy chuẩn Tilemap:** Kích thước tile 32x32 và cấu trúc layer trong Unity.

2. **[SpriteProcessorEditor.cs](SpriteProcessorEditor.cs)**
   * **Công cụ Unity Editor (C#):** Đặt vào thư mục `Assets/Editor/` của dự án Unity.
   * **Tính năng:** Tự động cắt lưới 128x128 cho hàng loạt spritesheet, cấu hình `Filter Point (no filter)`, `Uncompressed`, và tự động căn Pivot chuẩn chân nhân vật chỉ với 1 click.
