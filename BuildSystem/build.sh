echo "--Building for Windows x64"
env GOOS=windows GOARCH=amd64 go build -v -o ./buildSystem_windows_x64.exe
echo "--Building for Linux x64"
env GOOS=linux GOARCH=amd64 go build -v -o ./buildSystem_linux_x64
echo "--Building for Mac x64"
env GOOS=darwin GOARCH=amd64 go build -v -o ./buildSystem_mac_x64
echo "--Building for Mac arm64"
env GOOS=darwin GOARCH=arm64 go build -v -o ./buildSystem_mac_a64