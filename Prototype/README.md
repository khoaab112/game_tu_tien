# Multiplayer networking spike

## Chơi demo trên Windows

Mở thư mục `Prototype` trong Explorer và nhấp đúp `PlayDemo.cmd`. Launcher mở hai cửa sổ game và một server local chạy nền. Bấm vào từng cửa sổ rồi dùng WASD hoặc phím mũi tên. Đóng cả hai cửa sổ để dừng server. Nếu UDP 7770 đang bận, chạy `./Prototype/PlayDemo.cmd -Port 17770` từ gốc repo.

Sau **2 giây đi bộ liên tục**, nhân vật tự chuyển sang chạy và đổi hoạt ảnh. Giữ **Shift** khi di chuyển để chạy ngay. Dừng hẳn hoặc bị vật cản chặn đứng sẽ đặt lại thời gian đi bộ. Prototype dùng tốc độ QA **5,5** khi đi và **9** khi chạy. Game server đếm thời gian di chuyển, xử lý vị trí và gửi trạng thái chạy tới người chơi khác.

Bấm **F11** trong cửa sổ game để chuyển sang toàn màn hình. Bấm **F11** lần nữa để trở về kích thước cửa sổ trước đó. Bạn cũng có thể dùng nút phóng to trên thanh tiêu đề.

Bản đồ tân thủ dùng ảnh cập nhật `Assets/Art/StarterVillage.png` với hai vườn hoa và đường mòn ra rìa làng. Camera nhìn từ trên xuống, bám nhân vật đang điều khiển và hiển thị khoảng 1/5 diện tích bản đồ. Camera tự căn khi đổi kích thước cửa sổ và dừng ở mép làng. Người chơi xuất hiện ở quảng trường trung tâm. Server chặn biên bản đồ, nhà chính và lòng suối, với lối đi qua cầu gỗ. Các đường mòn kết thúc ở biên map hiện tại. Vùng chặn được vẽ xấp xỉ theo ảnh phẳng. Các đạo cụ nhỏ và phần che khuất nhân vật sau mái nhà hoặc cây cần dữ liệu riêng.

Nhóm `Wind foliage` đặt chín lớp hình thông và tre trên nền đã xóa cây. Mỗi lớp dùng một tư thế từ ảnh nguồn. Shader loại nền trắng JPEG và uốn dần tán cây theo nhịp gió riêng, giữ nguyên vùng gốc. Đây là hiệu ứng hình ảnh trên client, không có collider hoặc trạng thái mạng. Ảnh render ở `Artifacts/starter-village-wind.png`.

Nhân vật dùng bộ `Assets/Art/CharacterCycles` cho đứng, đi và chạy. Các chu kỳ đi và chạy có tám khung ở mỗi hướng trước, sau và ngang, căn theo mốc chân của từng hàng. Hướng trái lật từ hướng phải. Hoạt ảnh tiến theo quãng đường đã di chuyển và giữ nhịp qua lần chuyển đi–chạy. Client phản hồi hướng và tư thế bước ngay khi nhận input hợp lệ, còn server xử lý vị trí và kết quả đánh. Đoạn đấm vẫn dùng `Assets/Art/CharacterSlices` và phát khi server xác nhận đòn đánh. Các khung thủ thế, ngã, hồi phục, thiền và nằm đã nhập vào Unity để dùng khi có trạng thái tương ứng. GDD §00–01 vẫn để mở tên vùng, bố cục gameplay và kết nối vùng.

Bộ `CharacterCycles` dùng ImageGen tích hợp, lấy các khung dân thường người dùng cung cấp làm mẫu. Prompt cho ba hướng đi yêu cầu lưới 4 × 2 nền trong suốt, giữ khuôn mặt, tóc búi, áo nâu, quần sẫm, giày và góc nhìn. Thứ tự từ chân phải chạm đất, chịu lực, chân trái đi qua và đưa ra trước, sau đó lặp ngược chân để khép vòng. Prompt tư thế đứng yêu cầu ba ô trước, sau, ngang, cùng nhân vật, tỷ lệ và mốc chân với bộ đi.

Prompt ImageGen cho bộ chạy dùng từng sheet đi làm mẫu và giữ nguyên nhân vật, tỷ lệ cùng góc nhìn. Lưới 4 × 2 nền trong suốt mô tả chân chạm đất, khuỵu chịu lực, nâng gối và pha cả hai chân rời đất cho từng nửa bước. Lượt chỉnh hướng trước đảo tay chân ở hàng dưới và giữ cùng chiều vạt áo.

Bấm chuột vào bia xanh lá để chọn mục tiêu, rồi dùng Space khi đứng trong phạm vi 4 m. Hai cửa sổ thấy cùng lượng HP do server xác nhận. Bia tự hồi đầy sau 3 giây khi về 0 HP. Bia 100 HP, sát thương 25, nhịp đánh 0,6 giây và cách chọn mục tiêu là fixture QA cho §04–05. Quy tắc chọn mục tiêu, tầm đánh, hồi chiêu và né chiêu của game vẫn cần chốt.

Để mở project trong Unity Hub, thêm thư mục `Prototype`. Trong Editor, mở scene `Assets/Generated/Prototype.unity`, đóng các cửa sổ demo đang dùng UDP 7770, rồi bấm Play để tự chạy host và một client local trên cổng đó. Để thêm client vào host Editor, mở trực tiếp `Builds/EditorClient/TuTien.exe` nếu có bản này, hoặc dùng `Builds/Windows/TuTien.exe` sau khi build lại Windows. Hai nhân vật có màu theo ID. Nhân vật bạn điều khiển có nhãn `(You)`.

GDD §00, §10 và MVP_ROADMAP giai đoạn 1. Dữ liệu tốc độ, kích thước map và giới hạn vị trí chỉ phục vụ QA.

FishNet 4.7.4 ghim commit a2e2c3e12f5f4e18c96e402113e14030ae7849f0. Project dùng Unity 6.3 LTS (6000.3.29f1). Package Manager cần Internet và Git để tải dependency lần đầu.

## Build bằng PowerShell

Truyền đường dẫn Editor đã cài vào script. Thực hiện trong thư mục Prototype.

```powershell
./Build.ps1 -Editor 'C:/Program Files/Unity/Hub/Editor/<version>/Editor/Unity.exe'
./Smoke.ps1
./Build.ps1 -Editor 'C:/Program Files/Unity/Hub/Editor/<version>/Editor/Unity.exe' -Target LinuxServer
```

Editor cần giấy phép hoạt động. LinuxServer cần module Linux Dedicated Server. Script build tạo scene và prefab lặp lại được trong Assets/Generated. NetworkTransform được cấu hình server authoritative. Input gửi theo tick, server giới hạn vector/vị trí và ngừng di chuyển khi input bị gián đoạn.

Mặc định, smoke test chạy một Windows player headless làm server và hai client độc lập gửi input tự động. Bài thử đối chiếu ID nhân vật trong ba log để xác nhận server xử lý chuyển động và mỗi client thấy cả hai nhân vật di chuyển. Script chỉ dừng các tiến trình do chính nó tạo. Đóng Editor trước batch build.

Chạy `./Smoke.ps1 -ImpairedNetwork` để bật bộ mô phỏng FishNet trên server và cả hai client. Fixture QA thêm 75 ms cho chiều gửi của mỗi tiến trình và đặt xác suất mất gói unreliable là 5%. Với gói reliable, FishNet mô phỏng thời gian gửi lại bằng độ trễ bổ sung. Log client ghi RTT FishNet và tick rate.

Sau khi build Windows, đóng Editor đang mở cùng project rồi chạy `./SmokeEditorHost.ps1 -Editor <đường-dẫn-Unity.exe>`. Bài thử tự vào Play Mode, mở một client Windows và đối chiếu chuyển động theo ID. Script đóng cửa sổ client, kiểm tra host đã gỡ nhân vật, rồi mở lại client trên cùng host để kiểm tra chỉ sinh một nhân vật mới và hai phía vẫn thấy nhau di chuyển. Cổng QA mặc định là 17773.

Để chơi tay, chạy Builds/Windows/TuTien.exe cho từng client sau khi mở server bằng cờ --server -batchmode -nographics. Dùng WASD hoặc phím mũi tên. Kết nối loopback UDP 7770 cho bài thử local.

## Kiểm thử Linux Dedicated Server

Sau khi build với `-Target LinuxServer`, đưa toàn bộ thư mục `Builds/LinuxServer` sang môi trường Linux x86-64 dành cho QA. Mở UDP 7770 giữa client và server, rồi chạy trong thư mục build:

```sh
chmod +x TuTien.x86_64
./TuTien.x86_64 --server -batchmode -nographics -logFile "$PWD/server.log"
```

Khi server sẵn sàng, chạy từ thư mục `Prototype` trên Windows. Thay địa chỉ ví dụ bằng IP hoặc hostname của server và dùng đường dẫn Windows đọc được tới log đang ghi trên Linux:

```powershell
./Smoke.ps1 -Address 192.0.2.10 -ServerLog 'X:/qa/server.log'
```

Dành phiên server cho hai client của bài thử. Script đọc phần log server phát sinh trong lần chạy và lưu lại cùng log client trong `Artifacts`. Khi dùng cổng khác, truyền `--port` cho server và `-Port` tương ứng cho script. Client chơi tay nhận `--address` và `--port`.

## Trạng thái

Ngày 22/11/2026 đã compile và build Windows thành công bằng Unity 6000.6.10f1. Sau rà soát, bài đo phía client lấy mốc vị trí tại OnStartClient, trạng thái báo chuyển động client/server được quản lý riêng, cấu hình smoke được đọc một lần lúc khởi tạo. Smoke test chạy lại PASS với một tiến trình server headless và hai client riêng, cả hai nhận được spawn và chuyển động của người chơi còn lại. Log mới nhất tại Artifacts/smoke-20261122-185451, log build tại Artifacts/build-Windows.log.

Ngày 22/11/2026, build Windows và Linux Dedicated Server đều thành công trong bản sao QA dùng Unity 6.3 LTS. Smoke test Windows PASS với server headless và hai client Windows độc lập. Mỗi client thấy hai nhân vật di chuyển, khớp ID với log server. Build và log 6.3 LTS nằm trong `.tools/Unity63QA/Prototype` ở gốc repo, với log smoke tại `Artifacts/smoke-20261122-193928` trong bản sao. Project chính đã nhận phiên bản Editor và cấu hình package đã kiểm chứng, bỏ bốn package 6.6 không dùng trong prototype.

Chế độ kết nối server chạy riêng cũng PASS với server Windows QA trên cổng 17771. Bài chạy Linux vẫn chờ môi trường Linux x86-64. WSL trên máy báo `REGDB_E_CLASSNOTREG`.

Bài thử local trên Unity 6.3 LTS PASS ở 30 tick/giây với cả hai cấu hình. RTT FishNet ghi nhận 0–33 ms khi chạy loopback mặc định và 200–233 ms khi bật mô phỏng. Log trong bản sao QA nằm ở `Artifacts/smoke-20261122-194702` và `Artifacts/smoke-20261122-194718`. Bài thử xác nhận cả hai client thấy chuyển động do server xử lý. Bài đo jitter riêng, băng thông và sai lệch vị trí vẫn còn mở.

Bài thử đóng cửa sổ và vào lại cùng host Editor PASS trên Unity 6.3 LTS, với log bản sao QA tại `Artifacts/editor-host-20261122-200938`. Kịch bản kiểm tra đóng cửa sổ bình thường. Mất mạng đột ngột và thời gian từ input đến chuyển động hiển thị còn chờ đo riêng.

Bài thử bia tập local PASS trên Unity 6.3 LTS. Hai client gửi một đòn mỗi người, server ghi 75 rồi 50 HP, cả hai client thấy 50 HP. Log bản sao QA tại `Artifacts/combat-20261122-201404`. Bài tự động xác minh xử lý đòn đánh và đồng bộ HP. Thao tác chọn bia bằng chuột vẫn cần chơi thử trực quan.

Build Windows với sân đá và mô hình tu sĩ PASS trên Unity 6.3 LTS. Hai client tiếp tục thấy nhân vật của nhau di chuyển. Log bản sao QA tại `Artifacts/smoke-20261122-202500`. Ảnh render camera và ảnh cận mô hình ở `Prototype/Artifacts/starter-valley.png` và `Prototype/Artifacts/cultivators.png`.

Build sprite dân thường PASS trên Unity 6.3 LTS. Hai client ghi nhận pose bước ở nhân vật mình và nhân vật phía bên kia. Khi đánh bia, mỗi client ghi nhận pose đấm của nhân vật mình và cùng thấy bia còn 50 HP. Log bản sao QA ở `Artifacts/civilian-20261122-203507`, ảnh xem trước ở `Prototype/Artifacts/civilian-preview.png`.

Build nền làng tân thủ ban đầu PASS trên Unity 6.3 LTS. Hai client tiếp tục thấy nhau di chuyển. Kiểm tra tọa độ xác nhận điểm xuất hiện hợp lệ, nhà và suối chặn di chuyển, cầu gỗ đi qua được.

Log cả ba tiến trình có dòng Curl error 35 (connection reset). Smoke test loopback vẫn hoàn tất, chưa xác định nguồn request HTTP gây lỗi này. Chưa nghiệm thu cảm giác điều khiển tay, hình ảnh, mạng Internet hoặc tải đông người. Prediction/reconciliation, vật cản/pathing, combat và AOI theo khoảng cách thuộc các gói kế tiếp. Camera và cách điều khiển vẫn là lựa chọn của bản thử QA.
