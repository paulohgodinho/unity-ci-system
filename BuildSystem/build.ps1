Write-Host "--Building for Windows x64"
$Env:GOOS="windows"
$Env:GOOSGOARCH="amd64"
go build -o ./buildSystem.exe

Write-Host "--Building for Linux x64"
$Env:GOOS="linux"
$Env:GOOSGOARCH="amd64"
go build -o ./buildSystem_linux_x64

Write-Host "--Building for Mac x64"
$Env:GOOS="darwin"
$Env:GOOSGOARCH="amd64"
go build -o ./buildSystem_mac_x64

Write-Host "--Building for Mac arm64"
$Env:GOOS="darwin"
$Env:GOOSGOARCH="arm64"
go build -o ./buildSystem_mac_a64