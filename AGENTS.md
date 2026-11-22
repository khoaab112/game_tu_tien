# GAME TU TIÊN

- Quy tắc gameplay lấy từ [Master GDD](docs/GDD/MASTER_GDD.md). Chỉ sửa tài liệu này khi chủ dự án yêu cầu chốt hoặc thay đổi quyết định.
- Với mỗi gói việc, dùng [SYSTEM_INVENTORY](docs/GDD/SYSTEM_INVENTORY.md) để định vị mục GDD liên quan rồi đọc trực tiếp mục nguồn. Tra [MVP_ROADMAP](docs/GDD/MVP_ROADMAP.md) để đối chiếu phạm vi và nghiệm thu.
- Ghi rõ điểm mở ảnh hưởng đến công việc. Dữ liệu dùng để thử kỹ thuật chỉ nằm trong môi trường thử và được đánh dấu. Giữ quyền xác nhận kết quả gameplay trên game server.
- Khi dùng Antigravity, giao từng gói nhỏ với mục tiêu, mục GDD liên quan và tiêu chí nghiệm thu. Khi Codex và Antigravity cùng làm, giữ phạm vi file riêng. Tóm tắt file sửa, kết quả kiểm tra và phần còn vướng sau mỗi gói.
- Ưu tiên dùng art chủ dự án cung cấp và tái sử dụng asset sẵn có. Khi thiếu art cho một động tác nhiều hướng, chỉ dùng ImageGen để làm một hướng mẫu trước. Gửi bản xem trước và chờ chủ dự án duyệt mẫu rồi mới tạo các hướng còn lại.

## Gói triển khai đầu tiên

Theo [MVP_ROADMAP, giai đoạn 1](docs/GDD/MVP_ROADMAP.md#thứ-tự-triển-khai-và-kiểm-chứng), dựng một bản đồ thử dùng Unity 6.3 LTS và FishNet. Điều kiện nghiệm thu đầu tiên là hai client Windows độc lập kết nối cùng một dedicated server và thấy nhau di chuyển, với vị trí được server xác nhận. Sau khi chạy được cục bộ, kiểm tra Linux Dedicated Server. Combat, backend và lưu nhân vật thuộc các gói tiếp theo.
