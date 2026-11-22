# Roadmap prototype và MVP GAME TU TIÊN

Đề xuất dựa trên [Master GDD v0.5](MASTER_GDD.md), [danh mục hệ thống](SYSTEM_INVENTORY.md), [phân tích kỹ thuật](TECHNICAL_ANALYSIS.md) và [stack đề xuất](TECH_STACK_PROPOSAL.md). Đây là lộ trình theo điều kiện nghiệm thu. Lịch và ngân sách phụ thuộc nhân sự, chất lượng asset, tốc độ phê duyệt nội dung và kết quả thử mạng.

## Phạm vi đề xuất

| Mốc | Nội dung | Câu hỏi cần trả lời |
|---|---|---|
| **Prototype kỹ thuật** | Một vùng xám dùng chung, các client độc lập, di chuyển và target, đòn cơ bản, kỹ năng né được theo fixture, quái server điều khiển, đo latency và quan sát mạng. | Hai người có thấy cùng kết quả khi farm và giao chiến? Người chơi còn đọc được tín hiệu và né trong điều kiện mạng mục tiêu? |
| **MVP nội bộ** | Một vùng mở dùng chung. Vòng lặp đăng nhập → farm dược liệu/quái → chọn A đúng mốc Luyện Khí Trung Kỳ → học/trang bị công pháp → dùng đan, nhặt đồ → chạm bình cảnh → đánh Boss Độ Kiếp Luyện Khí→Trúc Cơ → đăng nhập lại kiểm tra tiến độ. Thử PvP và Auto Battle online, đo Boss khu vực theo bài tải riêng. | Vòng lặp có chơi được từ đầu đến cuối, có vui khi farm chung, và có đủ tin cậy để giữ XP, kho đồ, trạng thái Boss và phần thưởng? |
| **Phát hành đầu theo GDD** | Level 100 ở Nguyên Anh Hậu Kỳ Đại Viên Mãn. Dự trù thêm môn phái, Kết Đan, phi kiếm/phôi B, Nguyên Anh và mở B theo §11A. Hóa Thần và dung luyện C ở phần mở rộng theo GDD. | Công suất sản xuất nội dung, cân bằng, năng lực vận hành và phạm vi tính năng công khai đã đủ qua playtest chưa? |

### Các phần triển khai trong MVP

- **Lõi bắt buộc** — Dedicated server, đăng nhập thử, nhân vật bền, di chuyển đồng bộ, chiến đấu Hybrid, quái/spawn, điều khiển cùng vùng, XP/cảnh giới, A đúng mốc, công pháp tối thiểu theo slot, túi, thưởng quái và một lộ trình Độ Kiếp đầu. Nội dung mẫu lấy từ quyết định đã duyệt hoặc chạy dưới dạng fixture QA trong môi trường thử.
- **Lõi MMORPG cần kiểm chứng trước khi kết thúc MVP** — cạnh tranh farm trên cùng vùng, có phiên thử PK theo luật được duyệt cho test, Auto Battle PvE theo luật online và ít nhất một trận Boss khu vực để kiểm tra điểm nóng. Có thể chạy bài thử Boss trong phòng thử tải, độc lập với Boss Độ Kiếp. Cần quyết định luật cho trận Boss mẫu trước khi nghiệm thu thưởng, hàng chờ và tổ đội.
- **Mở rộng trong MVP nếu dữ liệu đã sẵn sàng** — nội tại Hộ Thuẫn sau Trúc Cơ, lĩnh ngộ từ tác động hợp lệ, và thử tham ngộ online/offline. Khung cường hóa được kiểm chứng bằng một ca giao dịch và phục hồi QA khi cần, lịch mở cho người chơi phụ thuộc bảng cường hóa đã duyệt.
- **Dời sau MVP** — thế giới nhiều vùng, tuyến cốt truyện dài, trọn lộ trình môn phái/trợ cấp và các cảnh giới tiếp theo, B, C, phi kiếm, tọa kỵ/trận pháp đầy đủ, Boss thế giới/phụ bản/bí cảnh, nghề và loop kinh tế hoàn chỉnh, shop tiền thật, tiền cao cấp, xử lý PK Nghiệp Phạt đầy đủ, chu kỳ class, Thiên Đạo theo vùng và bang hội. Giao dịch theo trạng thái khóa hiện tại. Mở rộng PK/Auto Battle sang PvP hoặc hoạt động đặc biệt sau khi có luật.

## Thứ tự triển khai và kiểm chứng

| Giai đoạn | Đầu ra và phụ thuộc | Điều kiện qua mốc |
|---|---|---|
| **0. Chuẩn bị quyết định và môi trường** | Chốt giả định thử PC Windows, Linux server, góc nhìn prototype, phạm vi fixture. Lấy quyết định thiết kế còn thiếu theo từng gói bên dưới. Chuẩn bị asset nguyên gốc/được phép sử dụng. | Mỗi gói có mục GDD, dữ liệu cần duyệt, quyết định kỹ thuật, phạm vi test và người chịu trách nhiệm. |
| **1. Spike game server và mạng** | Dựng build Windows/Linux, game loop authoritative, client độc lập vào cùng vùng, movement prediction và observer. Kiểm thử mô phỏng RTT/jitter/mất gói. | Kết quả authoritative nhất quán giữa các client. Có số liệu tick, băng thông và hiệu chỉnh vị trí. Có kết luận giữ FishNet hoặc chuyển sang phương án dự phòng. |
| **2. Vòng lặp farm và tiến độ bền** | Hạ quái dùng chọn mục tiêu và né; thêm XP, chọn A, loadout, rơi đồ/vào túi, đăng nhập lại. Backend, DB và dữ liệu gameplay có version. | Không cấp XP/thưởng trùng khi gửi lại lệnh, ngắt mạng hoặc reload. Tiến độ và A giữ đúng sau đăng nhập. Bài thử hai người tranh cùng quái có nguồn kết liễu nhất quán. |
| **3. Độ Kiếp đầu và Auto Battle PvE** | Tái dùng resolver chiến đấu, thêm trạng thái Độ Kiếp, Khinh Bỉ/Chán Ghét, kết quả và XP chờ. Chạy PVE auto với cấu hình ưu tiên, đan, camp và hồi sinh trong phạm vi §15. | Kịch bản thắng/thua, lần chạm 50%, xóa chu kỳ và ca phần thưởng lần đầu có kết quả đúng. Auto dừng chiến đấu khi server nhận mất kết nối và phiên tiếp theo không bị ghi đè bởi phiên cũ. Xác nhận phạm vi thẻ shop và ngắt mạng của Độ Kiếp trước khi mở toàn bộ nhánh cho tester. |
| **4. Kiểm chứng PK và điểm nóng Boss** | Thử PK trong vùng theo chính sách test đã duyệt. Boss khu vực dùng sổ đóng góp, tổ đội, hàng chờ và thưởng kép khi đã có công thức mẫu được duyệt. Chạy một đợt tải farm phân tán, một đợt PK/Boss tập trung và một đợt kéo dài. | Hit được xử lý nhất quán, một người chỉ nhận một lần cho từng loại thưởng. Tham gia muộn/rời trận/hồi HP đúng GDD. Có báo cáo mức tải đo được theo người cùng vùng, người cùng cụm, quái, hiệu ứng và cấu hình phần cứng. |
| **5. Alpha kín và đánh giá mở rộng** | Chạy hết vòng lặp, đo thời gian và lỗi thực tế, khôi phục DB/restart vùng, quan sát hành vi farm và tạo danh sách nội dung đầu phát hành. | Không có lỗi mất/nhân đồ hoặc sai tiến trình ở các ca đã duyệt. Có ngân sách tick/mạng mục tiêu, số liệu phục hồi và quyết định có mở thêm vùng hoặc triển khai nội dung tiếp theo. |

### Quyết định thiết kế cần từ chủ dự án trước từng gói

| Trước gói | Quyết định còn thiếu |
|---|---|
| Spike và combat | Kiểu điều khiển, target lock, cách né chiêu và quan hệ với Né tránh chỉ số. Dữ liệu kỹ năng/quái mẫu và hằng số cần dùng nếu áp dụng công thức phòng thủ. Fixture kỹ thuật có thể kiểm chứng mạng trước lúc duyệt trị số game. |
| Farm và túi | Đường cong XP đoạn MVP, mẫu công pháp, quái, vật phẩm và bảng rơi, ai được thưởng quái thường khi nhiều người đánh, hành vi túi đầy, cấu hình giới hạn túi cho bản chơi. |
| Độ Kiếp | Boss thử, dữ liệu vào trận/chuẩn bị, xử lý nguyên liệu và hư hỏng, XP chờ và trạng thái khi bị giáng, phạm vi Auto Battle, mất kết nối và thẻ ở bản thử. Các mốc và tỷ lệ Khinh Bỉ đã ghi dùng nguyên bản. |
| Auto Battle | Bán kính, ưu tiên mặc định, dữ liệu đan/ngưỡng, thời gian hồi sinh và hành vi khi điểm farm thay đổi. |
| Boss khu vực và PK | Công thức và trọng số đóng góp, hàng chờ, khóa đội, thuật toán đồ hiếm, phân phối khi vắng mặt/túi đầy, luật PK test và tác động tiến độ công pháp khi kết liễu người. |
| Sau alpha kín | Nền tảng phát hành, định hướng mỹ thuật/camera, CCU mục tiêu cho vùng/điểm nóng, thị trường và ngân sách, phạm vi content/LiveOps tới level 100. |

Trong từng milestone cần ghi dữ liệu đã duyệt và bài thử tương ứng. Chỉ đưa vào build dành cho tester các đường chơi có đủ luật và thông số đã duyệt. Xác nhận mục tiêu đo và ngưỡng đạt trước khi so sánh phiên bản. Các phép đo kỹ thuật như số client giả lập và profile mạng thuộc cấu hình bài thử.

## Tận dụng Antigravity để giảm ngữ cảnh lặp lại

Đã xác nhận Antigravity IDE có trên máy Windows. Đề xuất dùng từ giai đoạn triển khai với [Artifact Review](https://antigravity.google/docs/artifact-review) và [Workspace Rules](https://antigravity.google/docs/ide/rules). Cách này giảm lượng nội dung lặp lại trong mỗi phiên bằng cách giao đúng mục GDD và đường dẫn cần đọc. Mức tiết kiệm token tùy mô hình, phiên bản IDE, lượng file được mở và số lần phải sửa.

1. Giao một gói có biên rõ như build headless, phép thử observer hoặc kiểm thử một quy tắc CD. Mỗi prompt kèm mục GDD, đoạn phân tích kỹ thuật liên quan, file được phép sửa và nghiệm thu. Dùng [danh mục hệ thống](SYSTEM_INVENTORY.md) làm mục lục để đọc đúng đoạn nguồn.
2. Dùng Planning Mode/Request Review cho thay đổi quyền mô phỏng, cấu trúc dữ liệu bền hoặc quy tắc có tương tác nhiều hệ. Việc hạn chế phạm vi gói giúp Antigravity đọc ít file và dễ kiểm tra diff. Khi có nhiều phiên, ưu tiên phạm vi file độc lập để tránh sửa chồng.
3. Sau mỗi gói, lưu kết quả thử và quyết định kỹ thuật ngắn trong tài liệu/PR của gói. Dùng lại các kết quả này ở giai đoạn sau. Khi gặp khoảng trống nghiệp vụ, ghi mục/dòng và tác động, đánh dấu chờ quyết định rồi tiếp tục các phần độc lập. Master GDD chỉ cập nhật theo xác nhận của chủ dự án.

Mẫu giao việc cho giai đoạn đã được phép triển khai:

> Gói việc §13 dòng 310–316. Triển khai và kiểm thử thời gian thực hiện cùng CDR theo công thức đã chốt. Đọc đoạn GDD và hàng Chỉ số/Tốc độ trong hai tài liệu phân tích. Chỉ sửa module tính thời gian và phần test liên quan. Dùng fixture QA cho dữ liệu thử. Nộp kế hoạch ngắn, diff, kết quả kiểm thử và điểm cần quyết định.

Toàn bộ roadmap này thuộc giai đoạn phân tích. Các bước build, cấu hình Antigravity, tạo repository và triển khai hạ tầng thuộc giai đoạn tiếp theo.
