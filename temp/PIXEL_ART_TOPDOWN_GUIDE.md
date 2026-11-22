# ĐẶC TẢ KỸ THUẬT ĐỒ HỌA TOP-DOWN PIXEL ART (128x128 XIANXIA)

Tài liệu này chuẩn hóa toàn bộ thông số đồ họa theo phong cách mẫu từ **Meowa Showcase sc_gen_7269** (Hiệp khách áo đen nón lá viền ngọc bích, dải Chân Khí phát sáng), chuyển đổi sang góc nhìn **Top-Down (3/4 Overhead Perspective)** cho game Tu Tiên.

---

## 1. Quy chuẩn Nhân vật & Animation (Character Sprites)

* **Kích thước khung hình (Frame Size):** `128 x 128 px`
* **Điểm neo (Pivot Point):** `Custom (X = 0.5, Y = 0.12)` — đặt ngay tại chân nhân vật tiếp đất để logic di chuyển, tính va chạm (Collider) và hiệu ứng bóng đổ chính xác.
* **Tốc độ khung hình (Animation FPS):** `8 - 12 FPS` (chuẩn nhịp pixel mượt mà, đậm chất điện ảnh).
* **Góc nhìn (Perspective):** **Top-Down 3/4** (camera chếch ~45° từ trên xuống, thấy rõ nón đấu lập, vai, vạt áo và mặt đất phía trước).
* **Hướng di chuyển (Directions):**
  * **Chuẩn 4 hướng:**
    * `Down` (Nhìn thẳng về phía màn hình - góc chính diện 3/4)
    * `Up` (Quay lưng lên trên)
    * `Side` (Trái / Phải - chỉ cần vẽ 1 bên, dùng code lật sprite `flipX` trong Unity)
  * **Nâng cao 8 hướng (khi mở rộng):** Bổ sung 4 hướng chéo (`Down-Right`, `Down-Left`, `Up-Right`, `Up-Left`).

---

## 2. Bảng màu chuẩn (Palette Hex) & Ngũ Hành

### Bảng màu gốc (Phong cách Đạo Thể Ngọc Bích)
* **Y phục & Nón (Dark Ink):** `#18191f`, `#23272e`, `#383e4a`
* **Viền áo & Họa tiết (Dark Teal):** `#1e4d40`, `#2b6e5c`, `#48a68b`
* **Chân Khí Ngọc Bích (Jade Qi Glow):** `#2dd4bf`, `#5eead4`, `#ccfbf1`
* **Màu Da:** `#fbe5d6`, `#e8bfa8`

### Bảng màu biến thể theo Ngũ Hành (Dùng cho Phái / Tu Vi khác nhau)
| Hệ | Tên gọi | Màu Chân Khí (Qi Aura) | Mã Hex gợi ý |
| :--- | :--- | :--- | :--- |
| **Mộc (Gốc)** | Thanh Ngọc Đạo Khí | Ngọc bích / Xanh lơ (Teal / Jade) | `#2dd4bf`, `#5eead4` |
| **Hỏa** | Tam Muội Chân Hỏa | Cam đỏ / Đỏ thẫm lửa | `#f97316`, `#ef4444` |
| **Kim / Lôi** | Cửu Thiên Lôi Kiếp | Vàng điện quang / Trắng vàng | `#eab308`, `#fef08a` |
| **Thủy / Băng** | Huyền Băng Hàn Khí | Lam tuyết / Xanh đá lạnh | `#38bdf8`, `#bae6fd` |
| **Ma Đạo** | Cửu U Ma Khí | Tím sẫm / Đỏ đen ma quái | `#a855f7`, `#7e22ce` |

---

## 3. Bộ Prompts mẫu tạo Animation Top-Down trên AI (Meowa / Midjourney / SD)

Dùng các prompt này để sinh ảnh/animation giữ đúng phong cách nhân vật:

### A. Di chuyển hướng xuống (Walk Down - Nhìn trực diện Top-down)
```text
128x128 pixel art character spritesheet, top-down 3/4 overhead perspective. An ancient Chinese Xianxia swordsman in black hanfu robes with dark teal trim, wearing a conical bamboo straw hat. Walking forward down towards camera. Flowing robes and hair, vibrant luminous jade cyan Qi energy trails swirling around the body, floating glowing particles. Clean dark outlines, cluster shading, crisp pixels, white solid background.
```

### B. Di chuyển ngang (Walk Side - Nhìn ngang Top-down)
```text
128x128 pixel art character spritesheet, top-down 3/4 overhead perspective. An ancient Chinese Xianxia swordsman in black and jade robes, side view walking cycle. Wide sleeves billowing gently, conical straw hat tilted, glowing teal energy ribbons flowing backwards. Smooth pixel animation frames, dark outlines, isolated on white background.
```

### C. Di chuyển hướng lên (Walk Up - Quay lưng)
```text
128x128 pixel art character spritesheet, top-down 3/4 overhead perspective, back view. An ancient Chinese cultivator with long black hair under a conical bamboo hat, black robes with teal jade patterns, walking away into distance. Helical jade energy trail wrapping around shoulders. Pixel art, crisp pixels, solid background.
```

### D. Tấn công / Tung chiêu (Sword Slash / Cast Qi)
```text
128x128 pixel art action spritesheet, top-down perspective. Xianxia swordsman unsheathing sword with a sweeping arc, crescent-shaped glowing jade sword wave slash VFX, spiraling Qi ribbons bursting outwards, dynamic combat pose, crisp pixel art, clean borders.
```

---

## 4. Quy chuẩn Môi trường / Bản đồ (Tilemap Top-Down)

* **Kích thước Tile:** `32 x 32 px` hoặc `64 x 64 px` (với tile 32x32, nhân vật 128x128 sẽ cao khoảng 2.5 - 3 ô gạch, tạo cảm giác nhân vật chi tiết trên nền bản đồ rộng).
* **Các lớp Layer trong Unity:**
  1. `Ground`: Mặt cỏ, đất nện, đá cuội, thềm đá hoa cương cổ trấn.
  2. `Ground_Decoration`: Cỏ lau, hoa dại, rêu phong, lá tre rơi.
  3. `Obstacles (Có Collider)`: Cột đình, cây tùng cổ thụ, vách đá, lư hương lớn, tượng sư tử đá.
  4. `Roof / Canopy (Trên đầu nhân vật)`: Mái ngói cong cổ trang, tán lá cây (có script làm mờ khi nhân vật đi bên dưới).
  5. `Emissive_VFX`: Đèn lồng phát sáng, tụ linh trận, suối linh tuyền.

---

## 5. Tách Layer hiệu ứng Chân Khí phát sáng trong Unity (Bloom VFX)

Để dải Chân Khí phát sáng rực rỡ như mẫu video:
1. Nhân vật chia làm 2 sprite (hoặc 1 sprite chính kèm 1 **Emissive Mask**).
2. Dùng script `temp/sprite_processor.py` để tự động tách mask phát quang từ sprite gốc.
3. Trong Unity URP: Gắn Material sử dụng shader hỗ trợ Emission (như `Universal Render Pipeline/2D/Sprite-Lit-Default`), gán Emissive Mask vào và bật `Bloom` trong Global Volume.
