docker run --rm -v "${PWD}:/usr/src/myapp" -w /usr/src/myapp golang:1.22.2-alpine3.19 sh -c "./build.sh"