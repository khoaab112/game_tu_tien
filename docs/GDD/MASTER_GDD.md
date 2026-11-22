# GAME TU TIÊN — MASTER GDD

**Phiên bản Master GDD:** v0.5 · **Khởi tạo:** 22/11/2026 · **Cập nhật:** 22/11/2026 · **Trạng thái:** Đang phát triển. **Linh Căn v0.1:** khung đã duyệt; **Tu luyện/Cảnh giới/Thiên Đạo/Độ Kiếp:** đã bổ sung; **Công pháp/kỹ năng/chỉ số chiến đấu:** đã bổ sung; **Auto Battle/Quái vật-Boss/Trang bị-cường hóa:** bổ sung tại v0.5.

**Mục đích:** Nguồn tham chiếu thống nhất cho các quyết định thiết kế game. Chỉ cập nhật nội dung khi chủ dự án yêu cầu chốt/thêm/sửa/xóa rõ ràng.

## Quy tắc quản lý quyết định

- **Đã chốt:** Có xác nhận rõ ràng từ chủ dự án, được ghi ở mục tương ứng.

- **Đang thảo luận:** Chưa đưa vào thiết kế chính thức.

- **Đã thay thế:** Quyết định cũ, lưu lại trong nhật ký để truy vết.

- Không suy diễn ví dụ minh họa, gợi ý của trợ lý hoặc phương án đang tranh luận thành thiết kế chính thức.

- Mỗi lần cập nhật: mô tả thay đổi, lý do (nếu có), ngày và phiên bản.

## 00. Tổng quan — các quyết định đã chốt

| Mã | Hạng mục | Quyết định | Trạng thái |
| - | - | - | - |
| DEC-001 | Thế giới | Thế giới mở; nhiều người cùng farm quái và PK | Đã chốt |
| DEC-002 | Chiến đấu | Hybrid: kết hợp chọn mục tiêu và né chiêu | Đã chốt |
| DEC-003 | Phát triển nhân vật | Có môn phái gốc; được học thêm công pháp ngoài | Đã chốt |

**Định hướng ý tưởng chưa chốt:** Game tu tiên góc nhìn từ trên xuống, gợi nhớ Võ Lâm Truyền Kỳ cũ; farm quái, săn item, ngũ hành, kỹ năng kết hợp và khắc chế. Các chi tiết này là brief ban đầu, cần thảo luận trước khi ghi thành quy tắc cụ thể.

## 01. Thế giới và cốt truyện

*Chưa có thiết kế chi tiết được chốt.*

## 02. Nhân vật, cảnh giới và môn phái

**Đã duyệt:** Nhân vật có môn phái gốc và có thể học công pháp ngoài (DEC-003). Nếu một trong hai linh căn cha mẹ A/B phù hợp hệ môn phái, nhân vật được **Gia Hộ Linh Căn** (buff chỉ số môn phái). Nếu cả A và B không phù hợp, không nhận buff Gia Hộ, thay bằng trợ cấp tài nguyên tu luyện từ tông môn có **giới hạn cấp phát theo ngày**, tránh farm vô hạn bằng tài khoản phụ.

**Chưa chốt:** Danh sách môn phái, hệ môn phái, giá trị gia hộ, điều kiện và mức trợ cấp, cảnh giới, thuộc tính nhân vật, giới hạn học công pháp.

## 03. Ngũ hành và khắc chế

### Khung hệ thống Linh Căn v0.1 — ĐÃ DUYỆT

**Giới hạn nhân vật:** Tối đa **3 linh căn**: **A — Linh Căn Bản Mệnh**, **B — Linh Căn Thứ Hai**, **C — Dị Linh Căn**. Tại một thời điểm chỉ tồn tại **một C**. Sau khi tạo C, A và B vẫn tồn tại, giữ nguyên buff linh căn cha mẹ. Không sinh linh căn thứ tư.

**Linh căn A:** Người chơi tự chọn Kim/Mộc/Thủy/Hỏa/Thổ ở **Luyện Khí Trung Kỳ** (thay cho thời điểm chọn khi khởi đầu trong v0.2); xem lộ trình mục 11A.

**Linh căn B:** Mở ở cấp độ/cảnh giới nhất định; **không được trùng A**. Phải có vật phẩm để đúc, có thể mua hoặc săn/farm. Người nạp tiền có thể lấy/chọn linh căn mong muốn thuận tiện, nhanh hơn; người cày chay có thể đạt cùng lựa chọn nhưng mất thời gian/tài nguyên. Điều kiện cấp độ mở khóa như nhau.

**Dung luyện C:** Đủ điều kiện tu luyện và học bí pháp dung luyện A+B. Mỗi cặp A+B có thể sinh ra nhiều chủng Dị Linh Căn; các kết quả trong cùng nhóm **có xác suất ngang nhau**. C bổ sung chiêu thức mới và buff riêng, vẫn phụ thuộc đặc tính hai linh căn cha mẹ.

**Tỷ lệ THÀNH CÔNG dung luyện:** Lần dung luyện C đầu tiên bắt đầu **10%**; mỗi lần thất bại tăng **10 điểm phần trăm** cho lần tiếp theo, tối đa **100%**. Tỷ lệ tích lũy giữ lại nếu người chơi dừng rồi quay lại. Thành công kết thúc chu kỳ. Nếu đã tẩy C và dung luyện lại, chu kỳ mới bắt đầu **5%** do thân thể bị tổn thương; mỗi thất bại vẫn tăng **10 điểm phần trăm**, tối đa **100%**.

**Tẩy Dị Linh Căn:** Có vật phẩm tẩy được bán trong shop (tên chưa chốt), dùng để xóa C và thử dung luyện lại một C khác. Cần **thời gian hồi phục** trước khi dung luyện lại. A/B và buff gốc được giữ nguyên.

**Buff C:** Dị Linh Căn có những dòng buff ngẫu nhiên (ví dụ ba dòng a/b/c). **Tổng tỷ lệ ban đầu không quá 30%**. Trong quá trình tu luyện có thể **gia cố**, tối đa **30% mỗi dòng** (ví dụ ba dòng tối đa 30% + 30% + 30%). Các con số này là giới hạn hiển thị của dòng buff, **không phải** công thức tổng sát thương hay phép cộng sức mạnh chiến đấu. Công thức tương tác và cân bằng được bàn sau.

### Những điểm CÒN MỞ — chưa tự quyết định

- Cấp độ/cảnh giới mở B/C; bí pháp cụ thể; nguyên liệu, chi phí, tên và số loại Dị Linh Căn từ mỗi cặp A+B.

- Cách phân bổ buff lúc ra C: tổng có luôn bằng 30% hay ngẫu nhiên tối đa 30%; số dòng theo từng chủng; tác dụng chính xác của từng dòng.

- Phương pháp gia cố: tăng riêng từng dòng, ngẫu nhiên hay đồng thời; xác suất thành công và nguyên liệu.

- Tẩy C đã gia cố có làm mất tiến độ không, có hoàn nguyên liệu hoặc chuyển tiến độ không.

- Vật phẩm tẩy có nguồn ngoài shop không; khả năng giao dịch; thời gian hồi phục và khả năng rút ngắn.

- Công thức chỉ số, quy tắc cộng dồn, tương sinh/tương khắc, khắc chế, tương tác công pháp và cân bằng PvP.

**Lưu ý:** Tên Dị Linh Căn, ví dụ phối hợp Hỏa/Thủy và các buff minh họa trong chat chưa phải danh sách chính thức.

### Bổ sung Ngũ Hành — quyết định chốt ngày 22/11/2026

- **Không có khắc chế cứng hoặc phản ứng nguyên tố:** Ngũ Hành không tự động tăng/giảm sát thương khi đánh vào hành khác. Hiệu ứng của các hành xử lý độc lập; ví dụ mục tiêu trúng hiệu ứng Thủy và Hỏa thì mỗi hiệu ứng hoạt động theo kỹ năng tương ứng, không sinh phản ứng phối hợp.

- **Đặc trưng Kim:** sát thương vật lý và sát thương chuẩn cao; tỷ lệ chí mạng và xuyên giáp tốt.

- **Đặc trưng Mộc:** hồi máu mạnh, giáp ảo lớn, trói chân tốt; sát thương vật lý và pháp thuật hỗn hợp.

- **Đặc trưng Thủy:** khống chế, giảm mạnh hồi HP/MP của địch, tự hồi phục tốt; thiên sát thương phép và sát thương duy trì ổn định.

- **Đặc trưng Hỏa:** sát thương chí mạng cao, sát thương duy trì mạnh trong giao tranh dài, hút máu HP tốt; khống chế và xuyên giáp yếu.

- **Đặc trưng Thổ:** giáp rất dày, khống chế cứng, hấp thụ sát thương rất mạnh, hồi HP/MP trung bình, đòn đánh thường mạnh; tổng sát thương trung bình, chí mạng và sát thương chí mạng thấp.

- **Phạm vi linh căn khi học công pháp:** Hai linh căn cha mẹ A/B là hai hành được dùng để chọn biến thể công pháp đại trà và xét điều kiện công pháp đặc thù thông thường. Dị Linh Căn C (hành con) có công pháp đặc thù riêng; không tính C vào điều kiện A/B.

- **Công pháp đặc thù không song hành:** Bỏ loại công pháp đòi đồng thời hai hành cha mẹ. Điều kiện Ngũ Hành của công pháp đặc thù thông thường chỉ cần một hành phù hợp thuộc A hoặc B. Các điều kiện khác nếu có sẽ thiết kế sau.

- **Chưa quyết định:** Trị số thưởng Ngũ Hành, công thức cân bằng và thiết kế từng công pháp/kỹ năng; không suy diễn các ví dụ thành thông số chính thức.

## 04. Chiến đấu và điều khiển

**Đã chốt:** Chiến đấu Hybrid, kết hợp chọn mục tiêu và né kỹ năng (DEC-002).

**Định hướng đã duyệt:** **Bài Xích Thiên Đạo** là hệ thống **buff/debuff class thay đổi theo chu kỳ trong thế giới game** nhằm điều chỉnh sức mạnh class. Không phải cơ chế trừng phạt riêng một người chơi quá mạnh.

**Chưa chốt:** Chu kỳ bao lâu, cách xác định class cần chỉnh, mức buff/debuff, ảnh hưởng PvP/PvE, cách thông báo, target lock, né, thời gian hồi và combo.

## 05. Công pháp và kỹ năng

### Khung công pháp và kỹ năng — đã chốt ngày 22/11/2026

- **Công pháp chung theo ngũ hành:** Cùng một cuốn công pháp có thể dùng với nhiều hệ ngũ hành; hiệu ứng/màu kỹ năng đổi theo hệ và thuộc tính chiêu thức được điều chỉnh phù hợp. Không mặc định mỗi hệ là một bộ kỹ năng hoàn toàn độc lập.

- **Ba vùng slot riêng:** (1) chủ động; (2) bị động, bao gồm chế tạo và điều chế; (3) nội tại. Mỗi vùng có số slot giới hạn theo đại cảnh giới.

- **Chủ động:** kỹ năng tấn công hoặc buff kích hoạt bằng thao tác; buff có thời hạn và CD, không cộng chỉ số thường trực.

- **Bị động:** hiệu ứng ở trạng thái sẵn sàng khi trang bị, tự kích hoạt theo điều kiện, có CD; có thể phản đòn, tăng sát thương kỹ năng chủ động, hỗ trợ điều trị hoặc tăng nhẹ chỉ số trong thời gian hiệu lực. **Chế tạo và điều chế thuộc vùng bị động:** hỗ trợ chỉ số/nâng cấp trang bị, tỷ lệ chế tạo/luyện đan thành công và tỷ lệ sản phẩm hiếm.

- **Nội tại:** tăng mạnh chỉ số nền tảng, duy trì khi trang bị; có vùng slot riêng.

- **Kỹ năng cơ bản:** có đúng **1 ô riêng**, **không tiêu hao mana**, không chiếm slot chủ động. Tối đa 7 ô chủ động + 10 ô bị động + 5 ô nội tại + 1 ô cơ bản = **23 ô**.

- **Bảng slot theo đại cảnh giới** (chưa tính ô cơ bản):

| Đại cảnh giới | Chủ động | Bị động | Nội tại | Tổng ba vùng |
| - | -: | -: | -: | -: |
| Luyện Khí | 2 | 3 | 0 | 5 |
| Trúc Cơ | 4 | 4 | 1 | 9 |
| Kết Đan | 5 | 5 | 1 | 11 |
| Nguyên Anh | 6 | 7 | 2 | 15 |
| Hóa Thần | 7 | 7 | 4 | 18 |
| Luyện Hư | 7 | 9 | 5 | 21 |
| Hợp Thể | 7 | 10 | 5 | 22 |
| Đại Thừa | 7 | 10 | 5 | 22 |

- **Tu luyện/nâng cấp công pháp:** có hướng tăng cấp qua tu luyện, sử dụng và tiêu diệt mục tiêu; có thể dùng thêm công pháp làm nguyên liệu tăng cấp. Hạ quái tính 1 đơn vị quy đổi, hạ người chơi tương đương 50 quái. Kỹ năng kết liễu nhận tiến độ; nếu mục tiêu chết do thiêu đốt, độc hoặc hiệu ứng sát thương liên quan, quy về kỹ năng chủ động gần nhất đã tung ra.

- **Đổi công pháp khi đầy slot:** cần **Bùa Tẩy Công Pháp** bán bằng tiền, giá khá đắt; nâng cấp công pháp không dễ. Không tự suy diễn thêm quy tắc giữ/mất cấp khi tẩy.

- **PK và Thiên Đạo:** chấp nhận PK để tu luyện và con đường tà đạo; PK có thể tích điểm khiến Thiên Đạo truy xét, đến ngưỡng có thể bị giam cầm **hoặc** truy nã. Ngưỡng và hình phạt cụ thể chưa chốt; liên hệ hệ Thiên Đạo Nghiệp Phạt.

- **Giao dịch:** tạm khóa, cân bằng sau.

**Chưa chốt:** danh sách và cấp bậc công pháp, số liệu buff/CD, công thức tiến độ và cấp công pháp, cách phân bổ slot theo tiểu cảnh giới/cấp độ, quy tắc tẩy chi tiết, cơ chế Thiên Đạo trừng phạt PK, tương tác hiệu ứng trong trường hợp nhiều người, class/kỹ năng cụ thể, hệ chỉ số/ngũ hành, bản sắc môn phái và cân bằng PvP/PvE. Các chủ đề này được thảo luận riêng.

### Bổ sung cơ chế công pháp — quyết định chốt ngày 22/11/2026

- **Công pháp đại trà:** Giữ nguyên chỉ số gốc và kỹ năng cốt lõi của từng cuốn. Khi học, người chơi chọn **một** trong hai hành A/B để xác định biến thể; lựa chọn cố định sau khi học, không tự do đổi hành. Mỗi công pháp được chọn hành độc lập; một nhân vật có thể học đồng thời công pháp theo cả A lẫn B.

- **Biến thể Ngũ Hành:** Mỗi công pháp thiết kế riêng phần chỉ số bổ sung, hiệu ứng chiến đấu, dạng sát thương khi phù hợp và hoạt ảnh/màu kỹ năng theo hành đã chọn; không áp một bảng hiệu ứng hành chung cứng nhắc cho mọi công pháp.

- **Công pháp đặc thù:** Chỉ học được khi có hành yêu cầu trong A/B; không còn công pháp song hành yêu cầu đồng thời hai hành. Công pháp dành riêng cho Dị Linh Căn C là nhóm riêng.

- **Tiến độ tu luyện:** Học công pháp là bước mở đầu; chỉ số công pháp tăng dần theo mức tu luyện/lĩnh ngộ, không nhận ngay toàn bộ khi học. Giữ nguyên các nguồn tiến độ đã chốt phía trên (tu luyện, sử dụng, kết liễu, nguyên liệu).

- **Hiệu lực chỉ số:** Chỉ số từ công pháp **chỉ cộng khi công pháp đang được trang bị**; công pháp đã học nhưng không trang bị không cộng chỉ số. Quy tắc này thay thế phương án thảo luận trước đó về cộng chỉ số từ mọi công pháp đã học.

- **Tháo kỹ năng:** Kỹ năng chủ động được tháo; kỹ năng bị động được tháo mà không có hình phạt bất lợi bổ sung (đương nhiên không còn hiệu ứng của kỹ năng khi tháo). **Nội tại muốn loại bỏ phải dùng Tẩy Tủy Đan.**

- **Phân biệt vật phẩm:** Tẩy Tủy Đan cho nội tại là quyết định mới; tài liệu trước đó có **Bùa Tẩy Công Pháp** khi đầy slot. Chưa xác định hai vật phẩm có liên hệ hay thay thế nhau; tạm giữ riêng, không tự hợp nhất.

- **Giữ nguyên hệ thống slot theo cảnh giới** và ô kỹ năng cơ bản riêng đã ghi trong mục này. Chưa bổ sung loại ô trang bị công pháp mới.

- **Còn mở:** Quy tắc chi tiết về đổi biến thể nếu có cơ chế học lại đặc biệt; cách tính chỉ số theo cấp tu luyện; điều kiện và phạm vi áp dụng Tẩy Tủy Đan; danh sách và phẩm cấp công pháp.

## 06. PvE — quái vật, boss, bí cảnh

*Chưa có thiết kế chi tiết được chốt.*

## 07. Vật phẩm và trang bị

*Chưa chốt rơi đồ, độ hiếm, nâng cấp hay chế tạo.*

## 08. Kinh tế và giao dịch

**Đã duyệt trong khung Linh Căn:** Vật phẩm đúc B có thể mua hoặc farm; trả phí giúp chọn/đạt linh căn B mong muốn nhanh và thuận tiện hơn, còn người cày chay cần thêm thời gian/tài nguyên; cả hai có cùng yêu cầu cấp độ. Vật phẩm tẩy Dị Linh Căn C bán trong shop. Tông môn có thể trợ cấp tài nguyên tu luyện cho người không có A/B hợp môn phái, nhưng phải giới hạn cấp phát theo ngày để hạn chế tài khoản phụ khai thác vô hạn.

**Chưa chốt:** Định giá, loại tiền, khả năng giao dịch, nguồn vật phẩm ngoài shop, chi phí gia cố, số lượng và điều kiện cấp trợ cấp, quy tắc kinh tế tổng thể.

## 09. PvP/PK và tương tác người chơi

*Chỉ chốt có PK trong thế giới mở (DEC-001); chưa chốt luật PK, khu an toàn, hình phạt hay bang hội.*

## 10. Kỹ thuật và kiến trúc

*Chưa chốt engine, nền tảng, server hay cấu trúc dữ liệu.*

## 11. Lộ trình prototype và MVP

*Chưa chốt phạm vi, thứ tự tính năng hoặc mốc thời gian.*

## 11A. Hệ thống tu luyện, cảnh giới và Thiên Đạo — v0.3 (đã xác nhận khung, thông số mở ghi rõ)

### Cảnh giới và kinh nghiệm

- Chỉ có **level nhân vật** làm trục tiến triển; không có XP tu luyện riêng. Công pháp/kỹ năng có thể mở theo level hoặc bí kíp.

- Mốc đại cảnh giới: Luyện Khí 0–20; Trúc Cơ 20–40; Kết Đan 40–70; Nguyên Anh 70–100; Hóa Thần 100–150; Luyện Hư 150–230; Hợp Thể 230–300; Đại Thừa sau 300 (giới hạn tiếp theo chưa chốt). Mốc biên trùng nhau, cần đột phá để vào cảnh giới sau.

- Mỗi đại cảnh giới có Sơ/Trung/Hậu Kỳ; mốc tiểu cảnh giới tại 0%/40%/80% khoảng level; cuối cảnh giới là Hậu Kỳ Đại Viên Mãn. Buff tiểu cảnh giới ở mức vừa phải, trị số chưa chốt.

- Thiên Đạo Áp Chế Bình Cảnh: khi kẹt đột phá, chỉ 5% XP farm được tích vào XP chờ; trần XP chờ đến hết Sơ Kỳ cảnh giới kế tiếp, không vượt Trung Kỳ.

- Giai đoạn phát hành đầu hướng tới level 100 — Nguyên Anh Hậu Kỳ Đại Viên Mãn; Hóa Thần mở rộng tiếp, không tạo cơ chế chờ phiên bản riêng.

### Lộ trình nội dung và linh căn theo cảnh giới

- Luyện Khí Sơ: chưa có linh căn, farm dược liệu; Trung: chọn A trong Ngũ Hành; Hậu: học kiến thức và farm trước nguyên liệu phục vụ Kết Đan tương lai, chưa Kết Đan.

- Trúc Cơ Sơ: gia nhập môn phái, củng cố A; hỗ trợ từ môn phái cho đột phá Kết Đan khoảng 20% đã được nêu, cách tác động cụ thể chưa chốt.

- Kết Đan: A hoàn thiện; Trung mở phi kiếm; Hậu hình thành phôi B.

- Nguyên Anh: thiên kiếp làm A tổn thương; Trung mở B; Hậu có công pháp mạnh hơn.

- Hóa Thần: Sơ phong ấn 20% HP có thể hồi (không hồi vượt 80% HP lý thuyết); Trung giảm phong ấn còn khoảng 6–10% (chưa chốt số); Hậu dung luyện C từ A+B, là điều kiện tiến lên Luyện Hư.

- Những mốc mở A/B/C ở đây là **quyết định cập nhật sau khung Linh Căn v0.1**; các quy tắc xác suất, buff, tẩy C và giới hạn ba linh căn tại mục 03 vẫn giữ nguyên trừ khi được sửa rõ ràng.

### Phân loại Thiên Đạo

1. Thiên Đạo Áp Chế Bình Cảnh — XP chờ tại bình cảnh.

2. Thiên Kiếp/Độ Kiếp — khiêu chiến Boss để đột phá, có thể gây thương tổn linh căn, phong ấn HP hoặc debuff theo mốc truyện.

3. Thiên Đạo Bài Xích — bất lợi theo khu vực bản đồ (thiết kế chi tiết sau); **khác** Bài Xích Thiên Đạo theo chu kỳ class đã duyệt ở mục 04.

4. Thiên Đạo Ưu Ái — ưu đãi farm tài nguyên ở vùng đặc biệt (chi tiết sau).

5. Thiên Đạo Nghiệp Phạt — trừng phạt PK sai luật ngoài vùng cho phép (chi tiết sau).

6. Thiên Đạo Khinh Bỉ — suy yếu Boss Độ Kiếp sau khi người chơi chết; không buff người chơi.

7. Thiên Đạo Chán Ghét — hình phạt khi hết lượt thất bại được phép.

### Độ Kiếp — luật cốt lõi

- Mỗi Boss có độ khó/tỷ lệ thất bại gốc riêng phản ánh sức mạnh Boss. **Độ Kiếp là trận đánh thực tế**: thắng Boss thì đột phá; không có phép quay xác suất thắng/thua độc lập sau trận. Chuẩn bị pháp bảo, trận pháp, đan dược, công pháp, tọa kỵ và thực lực tốt hơn khiến Boss dễ đánh hơn; không phạt người chuẩn bị tốt. Công thức hỗ trợ chuẩn bị chưa chốt.

- Chết do Boss là được cộng Thiên Đạo Khinh Bỉ, **không yêu cầu sát thương tối thiểu**. Khinh Bỉ chỉ giảm chỉ số Boss, không tăng chỉ số nhân vật; mức suy yếu Boss tối đa 50% chỉ số gốc. Mức tăng theo cảnh giới thấp nhanh, cao chậm.

- Bảng Khinh Bỉ (mỗi lần chết / số lần chết chạm 50% / giới hạn lượt thất bại khi đạt 50%):

  - Luyện Khí→Trúc Cơ: +25% / 2 / 4.

  - Trúc Cơ→Kết Đan: +20% / 3 / 3.

  - Kết Đan→Nguyên Anh: +15% / 4 / 2.

  - Nguyên Anh→Hóa Thần: +10% / 5 / 1.

  - Hóa Thần→Luyện Hư: +8% / 7 / 1.

  - Luyện Hư→Hợp Thể: +6% / 9 / 1.

  - Hợp Thể→Đại Thừa: +5% / 10 / 1.

- Bộ đếm giới hạn bắt đầu khi **mức suy yếu gốc của Boss do Khinh Bỉ đạt 50%**; lần chết khiến chạm 50% **được tính là lượt thứ nhất**. Thay đồ không reset bộ đếm. Khi vượt kiếp thành công, xóa Khinh Bỉ và kết thúc chu kỳ.

- Thất bại thông thường không mất XP; nguyên liệu được hoàn lại một phần theo cảnh giới (tỷ lệ cụ thể chưa chốt); hư hỏng pháp bảo/tọa kỵ không được hoàn trả hoặc sửa miễn phí.

- Hết lượt ở mốc 50% mà thất bại, nếu không có thẻ bảo vệ thì Thiên Đạo Chán Ghét giáng nhân vật từ Hậu Kỳ Đại Viên Mãn về **Trung Kỳ Đại Viên Mãn**, không hoàn trả nguyên liệu lần cuối; **xóa Khinh Bỉ, reset bộ đếm và toàn bộ thời gian chờ**. Khi farm trở lại bình cảnh, bắt đầu chu kỳ mới.

- **Thẻ Trốn Tránh Phán Xét:** chỉ bán trong shop, không có nguồn farm; mỗi lần tránh Chán Ghét tiêu hao một thẻ, giữ cảnh giới và cho phép tiếp tục khiêu chiến; có thể dùng nhiều thẻ liên tiếp. Thẻ không xóa thời gian chờ và không hoàn trả hư hỏng.

- **Công Nhận Thực Lực:** thắng Boss ngay lần đầu của *mỗi lần đột phá đại cảnh giới* thì được phần thưởng tiêu hao nhỏ (ví dụ buff farm, x2/x3 XP), không phải nâng cấp sức mạnh vĩnh viễn.

### Thời gian chờ tái chiến

- Không chờ ở Luyện Khí→Trúc Cơ, Trúc Cơ→Kết Đan, Kết Đan→Nguyên Anh.

- Nguyên Anh→Hóa Thần: sau thất bại lần 1 chờ 20 phút, lần 2 chờ 40 phút, lần 3 chờ 60 phút, v.v.

- Hóa Thần→Luyện Hư: 40, 80, 120 phút; Luyện Hư→Hợp Thể: 80, 160, 240 phút; Hợp Thể→Đại Thừa: 160, 320, 480 phút; mỗi đại cảnh giới sau nhân đôi hệ số.

- Sau Chán Ghét reset toàn bộ thời gian chờ cùng chu kỳ. Việc đồng hồ chờ chạy khi offline là đề xuất triển khai, **chưa chốt**.

### Chưa chốt / cần cân bằng

Chỉ số và AI Boss; ý nghĩa kỹ thuật cụ thể của 'tỷ lệ thất bại gốc' (không được thêm vòng RNG sau chiến đấu); công thức tác động chuẩn bị lên Boss; tỷ lệ hoàn nguyên liệu theo cảnh giới; độ bền/hư hỏng; hiệu ứng thương tổn linh căn; xử lý mất kết nối; công thức buff, trang bị, kỹ năng sau khi bị giáng tiểu cảnh giới; kinh tế và giá thẻ; chi tiết các vùng Thiên Đạo; nội dung cảnh giới sau Nguyên Anh. Không tự điền các thông số này.

## 12. Nhật ký thay đổi

| Phiên bản | Ngày | Thay đổi | Nguồn quyết định |
| - | - | - | - |
| v0.1 | 22/11/2026 | Khởi tạo Master GDD; ghi nhận DEC-001, DEC-002, DEC-003 | Lựa chọn trực tiếp của chủ dự án |
| v0.2 | 22/11/2026 | Duyệt và đưa **khung Linh Căn v0.1** vào mục 02, 03, 04 và 08; giữ nguyên các điểm chưa chốt | Chủ dự án duyệt bản tổng hợp Linh Căn v0.1 |
| v0.3 | 22/11/2026 | Bổ sung hệ thống level/cảnh giới, lộ trình linh căn, Thiên Đạo, Độ Kiếp, Khinh Bỉ, Chán Ghét, thẻ bảo vệ và thời gian chờ; ghi rõ thông số còn mở; cập nhật thời điểm chọn A | Chủ dự án yêu cầu cập nhật sau khi đối chiếu tổng hợp |
| v0.4 | 22/11/2026 | Bổ sung khung công pháp, 3 vùng slot, bảng slot theo đại cảnh giới, ô kỹ năng cơ bản, tiến độ kết liễu, Bùa Tẩy Công Pháp và các điểm chưa chốt | Chủ dự án yêu cầu cập nhật |
| v0.5 | 22/11/2026 | Bổ sung Auto Battle, quái vật/Boss, đóng góp và phần thưởng Boss, trang bị/vật phẩm/cường hóa; ghi rõ các điểm còn mở | Các quyết định đã xác nhận trong thảo luận ngày 22/11/2026 |

**Quy ước:** Các cuộc trò chuyện là nơi thảo luận; Master GDD là nơi lưu thiết kế đã được phê duyệt.

## 13. Hệ thống chỉ số chiến đấu — quyết định bổ sung (22/11/2026)

**Trạng thái:** Các quy tắc dưới đây đã được chủ dự án xác nhận. Giữ nguyên các mục cũ; các điểm chưa chốt ghi riêng cuối mục.

### Chỉ số nhân vật và tài nguyên

- HP tối đa; Linh Lực tối đa; AD; AP; Giáp vật lý; Kháng phép; Hồi phục HP/giây; Hồi phục Linh Lực/giây; Tốc Đánh; Nhanh Nhạy; Tốc độ di chuyển.

- Tỷ lệ chí mạng; Sát thương chí mạng; Xuyên Giáp (%); Xuyên Kháng phép (%); Chính xác; Né tránh; Kháng chí mạng; Giảm sát thương cuối; Kháng hiệu ứng bất lợi; Giảm hồi chiêu (CDR); Hút HP; Hút Linh Lực; Tăng hiệu quả hồi phục.

- AD/AP cộng vào sát thương theo hệ số của từng kỹ năng (có thể kết hợp). Loại sát thương vật lý/pháp thuật/hỗn hợp do kỹ năng quyết định; sát thương hỗn hợp xử lý từng phần theo phòng thủ tương ứng.

- Hồi HP tự nhiên và hồi Linh Lực tự nhiên liên tục trong/ngoài PvP và PvE. Tăng hiệu quả hồi phục chỉ khuếch đại Hút HP, kỹ năng hồi máu và hiệu ứng hồi máu công pháp, **không tăng hồi HP tự nhiên**. Không có chỉ số phổ thông Giảm tiêu hao Linh Lực. Hút HP/Linh Lực chỉ kích hoạt trên kỹ năng được phép hút, tính theo sát thương thực tế.

- Xuyên phòng thủ chỉ theo %, không có xuyên giáp/kháng phép dạng điểm. Giáp và Kháng phép dạng điểm, giảm sát thương với hiệu quả giảm dần: **ST sau phòng thủ = ST trước phòng thủ × K/(K + phòng thủ hiệu dụng)**; K chưa chốt.

- Né áp dụng cho mọi sát thương trực tiếp; né thành công không nhận sát thương trực tiếp. **Né hiệu dụng = max(0, Né mục tiêu − Chính xác người đánh)**. Chính xác dư chuyển tối đa +10 điểm phần trăm tỷ lệ chí mạng; khi tỷ lệ chí mạng chạm 100%, phần thưởng dư đổi sang sát thương chí mạng theo tỷ lệ 2:1, tối đa +5 điểm phần trăm.

- Kháng chí mạng giảm phần sát thương **cộng thêm** do chí mạng, tối đa **70%**. Phần chỉ số vượt 70% chuyển thành giảm **mọi sát thương nhận vào** với hiệu suất 50% (ví dụ Kháng chí mạng 80% → giảm 70% phần cộng thêm chí mạng và giảm 5% mọi sát thương).

- Tăng/giảm sát thương cuối đối trừ nhau sau Giáp, Kháng phép và các loại kháng; không giới hạn cứng chỉ số Giảm sát thương cuối. Tăng sát thương cuối chỉ là hiệu ứng đặc biệt công pháp/kỹ năng/nội tại, không phải chỉ số trang bị phổ thông. Cách xử lý hệ số cuối bằng 0/âm chưa chốt.

### Tốc độ và hồi chiêu

- **Tốc Đánh:** rút ngắn ra đòn cơ bản và kỹ năng thuần vật lý. **Nhanh Nhạy:** rút ngắn niệm phép và khựng khi thi triển kỹ năng pháp thuật. **Kỹ năng hỗn hợp:** tốc độ tính bằng (Tốc Đánh + Nhanh Nhạy)/2.

- **Thời gian thực hiện = thời gian gốc/(1 + tốc độ/100)**. CDR độc lập, chỉ giảm thời gian chờ tái sử dụng, tối đa **70%**: CD thực tế = CD gốc × (1 − min(CDR,70%)).

- Tốc độ di chuyển độc lập và **không giới hạn tối đa**; không giảm thời gian thi triển.

### Hộ Thuẫn và Lá Chắn

- **Hộ Thuẫn:** công pháp nhập môn thuộc **Nội Tại**; mọi nhân vật có thể chọn trang bị khi có ô Nội Tại (từ Trúc Cơ theo bảng slot). **Hộ Thuẫn tối đa = (Giáp vật lý + Kháng phép) × 1,5**. Hồi **5% Hộ Thuẫn tối đa/giây** sau **10 giây không nhận sát thương PvP**; chỉ PvP đặt lại bộ đếm, sát thương quái/Boss không ngắt hồi.

- **Lá Chắn kỹ năng:** tồn tại trong thời gian riêng, hết hạn thì phần còn lại biến mất. Nhiều lớp từ cùng/khác kỹ năng được cộng dồn để hiển thị nhưng quản lý riêng giá trị và thời điểm hết hạn. Ưu tiên tiêu hao **Lá Chắn sắp hết hạn trước → Hộ Thuẫn nội tại → HP**. Sát thương dư tiếp tục sang lớp sau.

### Sát thương đặc biệt và hiệu ứng bất lợi

- **Sát thương chuẩn:** bỏ qua Giáp/Kháng phép, không chí mạng; vẫn chịu tăng/giảm sát thương cuối và bị Lá Chắn/Hộ Thuẫn hấp thụ.

- **Phản sát thương:** tính theo tỷ lệ của công pháp/kỹ năng × sát thương **thực tế trừ HP**, không tính sát thương bị chắn. Là sát thương chuẩn; không kích hoạt phản sát thương dây chuyền.

- **DoT:** không chí mạng; kỹ năng/hiệu ứng có thể né lúc áp dụng, nhưng khi đã dính thì các nhịp sau không thể né. Nhiều tầng từ cùng hoặc nhiều nhân vật đều cộng dồn; mỗi tầng có thời gian và sát thương riêng.

- **Kháng hiệu ứng bất lợi:** tối đa **70%**, không có miễn nhiễm hoàn toàn. Giảm **cường độ** đối với hiệu ứng định lượng; giảm **thời gian duy trì** đối với khống chế không có cường độ (choáng, trói...). Kỹ năng gây bất lợi có hồi chiêu.

- **Trọng Thương:** giảm **tất cả nguồn hồi HP**, bao gồm hồi tự nhiên. Mỗi người gây được tính riêng sau kháng hiệu ứng; nhiều hiệu ứng đồng thời kết hợp theo **phép nhân**: HP hồi = HP hồi gốc × tích(1 − hiệu lực từng nguồn).

- **Thanh Tẩy:** thuộc hệ thống công pháp; chi tiết loại và số hiệu ứng được tẩy để thiết kế sau.

### Chưa chốt / cần kiểm thử

- Hằng số K phòng thủ; kết hợp xuyên phòng thủ từ nhân vật/kỹ năng; hệ số chí mạng gốc; hệ số hút của kỹ năng diện rộng; chỉ số tăng sát thương kỹ năng/đòn cơ bản có phổ thông hay không; xử lý sát thương cuối bằng 0 hoặc âm.

- Kiểm thử Tank (Giáp/Kháng phép/Hộ Thuẫn/HP hồi), Kháng chí mạng dư, DoT nhiều tầng với CDR 70%, Trọng Thương nhiều nguồn, tốc độ cao và PvP nhiều người. Không tự đặt giới hạn mới khi chưa duyệt.

**Nhật ký thay đổi:** 22/11/2026 — Bổ sung mục 13 theo các quyết định chỉ số chiến đấu đã được xác nhận; không thay đổi các mục trước.

## 14. Phẩm cấp công pháp, thăng phẩm và lĩnh ngộ — chốt 22/11/2026

### Phẩm cấp và thăng phẩm — ĐÃ CHỐT

- Mỗi công pháp có phẩm cấp ban đầu và giới hạn phẩm cấp tối đa riêng. Có thể thăng phẩm đến giới hạn đó.

- Điều kiện thăng phẩm: **nguyên liệu + mức lĩnh ngộ**; không có điều kiện cảnh giới trực tiếp. Đủ điều kiện thì thành công **100%**, nguyên liệu được tiêu hao khi thành công.

- Thăng phẩm **giữ nguyên mức lĩnh ngộ và tỷ lệ % tiến độ trong mức**, quy đổi số điểm theo yêu cầu phẩm cấp mới.

- Thăng phẩm tăng chỉ số và có thể mở rộng hiệu ứng kỹ năng; không bắt buộc thêm hiệu ứng ở mỗi bậc. Hiệu ứng mới **tự động áp dụng** cho kỹ năng tương ứng khi đáp ứng điều kiện trang bị/sử dụng, không cần nâng kỹ năng riêng; giữ cơ chế cốt lõi.

- Điểm yêu cầu tăng theo phẩm cấp với **hệ số ×1,5 mỗi bậc**: XP(bậc n, bước m) = XP(bậc 1, bước m) × 1,5^(n−1).

### Lĩnh ngộ chiến đấu — ĐÃ CHỐT

- Lĩnh ngộ tăng qua **chiến đấu** và **tham ngộ chủ động theo thời gian**, cùng một tiến độ.

- Chỉ kỹ năng thuộc công pháp có **tác động hợp lệ** mới tạo lĩnh ngộ: gây sát thương trúng đích, hồi HP thực tế, áp hiệu ứng thành công, hoặc phòng thủ thực sự phát huy tác dụng. Bị né/không tác động thì không nhận. **Đòn đánh cơ bản không tạo lĩnh ngộ.**

- Không giới hạn lĩnh ngộ chiến đấu theo ngày, không giảm do chênh lệch sức mạnh mục tiêu; được farm quái yếu.

- Công pháp gốc nhận **100%** lĩnh ngộ. Các công pháp **khác đang trang bị** chia đều **quỹ bổ sung 30%** lượng lĩnh ngộ gốc. Tổng quỹ không phụ thuộc số công pháp; công pháp không trang bị không nhận chia sẻ. Nếu không có công pháp khác thì không phân phối quỹ.

- Quỹ chia sẻ 30% chỉ áp dụng cho chiến đấu, không áp dụng tham ngộ theo thời gian.

### Tham ngộ chủ động — ĐÃ CHỐT

- **Chỉ công pháp đang trang bị** được tham ngộ. Có thể tham ngộ nhiều công pháp cùng lúc; tổng tốc độ **chia đều khi thiết lập danh sách**. Tốc độ tham ngộ cơ bản **cố định như nhau cho mọi nhân vật**, không phụ thuộc trực tiếp cảnh giới, linh căn hoặc tư chất.

- Tham ngộ **đồng thời với chiến đấu**, không bị giảm tốc khi giao tranh.

- Tham ngộ offline tối đa **8 giờ tích lũy mỗi khoảng offline**; thời gian vượt mức không được bù.

- **Tháo công pháp:** tạm ngưng tham ngộ, giữ tiến độ và suất/tốc độ đã phân bổ, không chia lại; trang bị lại thì tự tiếp tục.

- **Chủ động xóa khỏi danh sách:** ngừng tham ngộ và giữ tiến độ, phần tốc độ cũ bỏ trống; **không tự chia lại** đến khi người chơi thiết lập lại danh sách. Khi thiết lập lại, tổng tốc độ chia đều theo danh sách mới.

### Buff tăng tốc tham ngộ — ĐÃ CHỐT

- Tài nguyên tăng **tổng tốc độ tham ngộ** bằng buff **có thời hạn**; các công pháp hưởng theo phân bổ hiện có, không tự phân bổ lại.

- Các buff **cộng trực tiếp %**, mỗi buff có thời gian hiệu lực độc lập; **trần +200%** (tối đa gấp 3 tốc độ gốc). Phần vượt trần không tăng hiệu quả.

- Buff hoạt động cả offline, thời gian hiệu lực vẫn trôi; lĩnh ngộ offline vẫn chỉ tích lũy tối đa 8 giờ. Buff hết hạn thì mất phần tăng tương ứng.

- Công pháp tạm ngưng không nhận lĩnh ngộ; phần tốc độ/buff phân bổ cho nó không chuyển sang công pháp khác.

### CHƯA CHỐT — không tự coi là luật

- Tên và số bậc phẩm cấp; phẩm cấp ban đầu/tối đa từng công pháp; nguyên liệu và ngưỡng lĩnh ngộ thăng phẩm cụ thể.

- Điểm yêu cầu gốc từng bước Nhập Môn → Tiểu Thành → Đại Thành → Viên Mãn; hệ số tăng giữa các mức; tốc độ tham ngộ cơ bản (điểm/giờ); điểm cho từng tác động hợp lệ; quy tắc làm tròn.

- Danh sách tài nguyên buff, mức tăng/thời hạn từng loại; xử lý khi thêm công pháp vào danh sách tham ngộ có suất trống. Các tên và con số minh họa trước đây không phải dữ liệu game.

**Nhật ký thay đổi:** 22/11/2026 — Thêm mục 14 tổng hợp các quyết định đã xác nhận về thăng phẩm, lĩnh ngộ, tham ngộ, offline và buff; không thay đổi các mục trước.

## 15. Auto Battle — chiến đấu tự động thời gian thực (chốt 22/11/2026)

### Kỹ năng và mục tiêu

- Người chơi tự sắp xếp thứ tự ưu tiên kỹ năng chủ động. AI kiểm tra từ ưu tiên cao xuống thấp; kỹ năng đang hồi chiêu, không đủ Mana hoặc không có mục tiêu hợp lệ thì bỏ qua. Không có kỹ năng khả dụng thì đánh thường.

- Không ngắt hành động đang thực hiện. Hoàn thành hành động hiện tại rồi mới kiểm tra lại danh sách ưu tiên.

- Người chơi thiết lập chế độ ưu tiên mục tiêu: gần nhất, HP hiện tại thấp nhất, % HP thấp nhất hoặc kẻ địch đang tấn công mình. Giá trị mặc định chưa chốt.

- Sau khi chọn, AI giữ nguyên mục tiêu đến khi mục tiêu chết hoặc không thể tấn công; sau đó mới tìm mục tiêu khác theo cấu hình.

### Di chuyển, hồi phục và đan dược

- Nhân vật tự tiếp cận mục tiêu trong phạm vi giới hạn tính từ vị trí bắt đầu chiến đấu. Khi không còn mục tiêu hợp lệ, quay về vị trí ban đầu rồi tiếp tục tìm mục tiêu.

- HP và Mana hồi phục tự nhiên liên tục trong/ngoài giao tranh.

- Tự động dùng đan dược theo cấu hình người chơi; mặc định bật theo ngưỡng HP/Mana. Hai ngưỡng độc lập và người chơi tự điều chỉnh.

- Đan hồi HP và đan hồi Mana có hồi chiêu độc lập; sử dụng tức thời, không hoạt ảnh và không làm gián đoạn hành động.

- Đan hồi phục theo thời gian; nhiều hiệu ứng cùng nhóm cộng dồn độc lập. Hồi phục tự nhiên vẫn chạy đồng thời. Khi HP/Mana đầy, hiệu ứng vẫn chạy và phần hồi dư bị mất.

- Khi HP thấp mà hết đan, nhân vật vẫn chiến đấu. Khi hết Mana, vẫn đánh thường và dùng kỹ năng đủ điều kiện.

### Chết, hồi sinh và online/offline

- Khi chết, hiệu ứng hồi phục từ đan vẫn tồn tại và thời gian tiếp tục trôi; hiệu ứng bất lợi bị xóa. Không mất tài nguyên.

- Tự hồi sinh sau thời gian cố định chung cho mọi nhân vật, với 50% HP và 50% Mana. Hồi sinh lặp lại không giới hạn.

- Sau hồi sinh, tự quay lại khu vực/vị trí treo máy, tìm mục tiêu và tiếp tục chiến đấu.

- Chủ động thoát game hoặc mất kết nối thì dừng chiến đấu ngay; không mô phỏng chiến đấu offline.

- Khi bản thân bị choáng, hồi chiêu vẫn đếm. Khi mục tiêu bị khống chế, AI tiếp tục tấn công bình thường.

- Phần thưởng quái tự động vào túi. Khi túi đầy, hành vi xử lý do người chơi cấu hình; lựa chọn cụ thể chưa chốt.

### Điểm còn mở

Phạm vi di chuyển theo hoạt động; giá trị mặc định của ưu tiên mục tiêu; thời gian hồi sinh; xử lý khi quay về mà khu vực thay đổi; tùy chọn khi túi đầy; quy tắc riêng cho PvP/hoạt động đặc biệt.

## 16. Quái vật và Boss — khung đã chốt (22/11/2026)

### Quái vật

- Ba loại: **Quái thường, Tinh anh, Boss**. Quái dùng hệ thống cảnh giới giống người chơi.

- Chỉ Boss có thuộc tính Ngũ Hành. Quái thường/tinh anh vẫn có thể có kỹ năng tùy loại.

- Quái chủ động hoặc bị động phát hiện/tấn công tùy thiết kế.

- Khi người chơi ra khỏi phạm vi truy đuổi, quái quay về vị trí ban đầu và hồi HP dần. Quái hồi HP/Mana tự nhiên như người chơi.

- Quái xuất hiện lại sau thời gian cố định theo từng loại.

### Boss

- Ba loại: **Boss khu vực, Boss phụ bản, Boss thế giới**.

- AI dùng kỹ năng theo điều kiện HP và tình huống; chuyển giai đoạn tùy Boss.

- Boss có thể triệu hồi quái hỗ trợ, có thể miễn nhiễm một số hiệu ứng khống chế tùy thiết kế và có thể tấn công nhiều mục tiêu bằng kỹ năng diện rộng.

- Khi toàn bộ người chơi tham chiến chết, Boss vẫn tồn tại và hồi HP dần. Giới hạn thời gian và thời gian tái xuất hiện tùy Boss/hoạt động.

### Đóng góp và phần thưởng Boss

- Điểm đóng góp gồm sát thương, chống chịu và hỗ trợ; trọng số tùy Boss/vai trò. Hồi máu chỉ tính HP thực tế hồi cho đồng đội trong trận Boss.

- Giữ toàn bộ sát thương đã gây ra vào điểm đóng góp, kể cả khi Boss hồi HP; hiện không giới hạn điểm từ sát thương lặp lại.

- Chỉ cần đóng góp lớn hơn 0 để đủ điều kiện nhận thưởng. Người chết, rời khu vực hoặc tham gia muộn vẫn giữ/được tính đóng góp.

- Đóng góp tổ đội chia đều cho tất cả thành viên; khóa thành viên tổ đội trong suốt trận Boss.

- Số người tham gia giới hạn theo từng Boss; người đến sau xếp hàng chờ vị trí trống.

- **Last Hit:** tất cả quái và Boss ghi nhận người kết liễu. Với Boss, người kết liễu nhận **thưởng Last Hit riêng**; người tham gia vẫn nhận thưởng theo đóng góp.

- Thưởng Boss tăng theo mức đóng góp; vật phẩm hiếm có xác suất nhận theo tỷ lệ đóng góp. Trao thưởng ngay khi Boss chết.

- Có thể đánh Boss vượt cảnh giới, không giới hạn chênh lệch.

### Điểm còn mở

Công thức quy đổi sát thương/chống chịu/hỗ trợ; cách tính đóng góp tổ đội trước khi chia đều; chống farm sát thương khi Boss hồi HP và chống đóng góp giả; số lượng/giá trị thưởng; thuật toán xác suất vật phẩm hiếm; giải phóng chỗ/hàng chờ; AI, chỉ số và kỹ năng cụ thể từng Boss.

## 17. Trang bị, vật phẩm và cường hóa — khung đã chốt (22/11/2026)

### Trang bị và vật phẩm

- Trang bị chia theo bộ phận cơ thể: **đầu, thân, tay, chân, trang sức, vũ khí**; số ô và danh sách slot cụ thể chưa chốt.

- Pháp bảo có ô riêng, độc lập trang bị cơ thể; số ô và điều kiện mở chưa chốt.

- Phẩm cấp: **Phàm → Hoàng → Huyền → Địa → Thiên → Tiên → Thần**.

- Được mặc trang bị vượt cảnh giới nhưng chịu phạt chỉ số; mức phạt phụ thuộc phẩm cấp trang bị. Công thức chưa chốt.

- Trang bị có thuộc tính cơ bản và dòng ngẫu nhiên; số dòng/khoảng trị số giới hạn theo phẩm cấp.

- Không có độ bền; không cho phép giao dịch trang bị.

- Kinh tế có **Linh Thạch** và một loại tiền tệ cao cấp; tên và cơ chế tiền tệ cao cấp chưa chốt.

- Chỉ nguyên liệu và vật phẩm tiêu hao được xếp chồng.

- Trang bị không dùng có thể phân giải hoặc bán cho NPC.

### Cường hóa

- Trang bị có thể cường hóa. Cấp tối đa tăng đều theo phẩm cấp, mỗi phẩm cấp thêm một số cấp cố định; trị số cụ thể chưa chốt.

- Dùng một loại nguyên liệu cường hóa chung; nguồn gồm phân giải trang bị, quái, Boss và hoạt động khác.

- Tỷ lệ thành công phụ thuộc cấp cường hóa hiện tại, phẩm cấp và vật phẩm hỗ trợ.

- Thất bại theo mốc: mốc thấp mất nguyên liệu; mốc giữa giảm đúng 1 cấp; mốc cao có thể phá hủy trang bị.

- Có một loại vật phẩm bảo vệ chống cả giảm cấp và phá hủy.

- Chuyển cường hóa chỉ giữa trang bị cùng vị trí; trang bị đích phải cùng hoặc cao hơn phẩm cấp nguồn. Chuyển toàn bộ cấp, trang bị nguồn bị tiêu hao; chi phí gồm Linh Thạch và nguyên liệu.

- Nếu trang bị bị phá hủy do thất bại, cấp cường hóa được giữ lại để chuyển sang trang bị khác.

### Điểm còn mở

Slot cụ thể/số ô pháp bảo; tên tiền tệ cao cấp; số dòng và trị số thuộc tính; công thức phạt vượt cảnh giới; cấp tối đa từng phẩm cấp; tỷ lệ thành công/chi phí; nguồn và độ hiếm vật phẩm bảo vệ; cách lưu cấp cường hóa sau phá hủy; chi tiết chuyển cấp; tỷ lệ phân giải/bán NPC.

## 18. Nhật ký thay đổi — v0.5 (22/11/2026)

- Bổ sung Auto Battle: ưu tiên kỹ năng, khóa mục tiêu, di chuyển giới hạn, hồi phục, đan dược, hồi sinh, online/offline và nhận thưởng.

- Bổ sung quái vật/Boss: phân loại, AI, đóng góp và phần thưởng; Last Hit Boss có thưởng riêng, không thay thế thưởng đóng góp.

- Bổ sung trang bị/vật phẩm/cường hóa: slot, phẩm cấp, thuộc tính, giao dịch, tiền tệ, xếp chồng, phân giải/bán NPC và chuyển cường hóa.

- Chỉ ghi quyết định đã được chủ dự án xác nhận; thông số/phương án còn mở được giữ riêng, không tự điền.
