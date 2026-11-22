# Gói rà soát prototype cho Antigravity

Đọc AGENTS.md, GDD §00/§10 và nguồn trong Prototype/Assets. Giới hạn gói này ở review và ghi kết quả vào docs/ANTIGRAVITY_FINDINGS.md.

Kiểm tra theo API FishNet tại commit đã ghim trong Prototype/Packages/manifest.json. Rà soát spawn sau khi client tải scene, quyền gọi ServerRpc theo owner, server-authoritative NetworkTransform, đăng ký/hủy OnTick, reconnect/despawn, bộ tạo prefab và Windows/Linux build. Đối chiếu các marker mà Prototype/Smoke.ps1 đang kiểm tra.

Mỗi lỗi cần có file/dòng, tình huống tái hiện và sửa đề xuất. Báo rõ phần nào đã compile/chạy thật, phần nào chỉ đọc nguồn. Codex phụ trách sửa và build trong Prototype, Antigravity phụ trách file findings của gói review.
