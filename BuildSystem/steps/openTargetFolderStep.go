package steps

import (
	"buildsystem/base"
	"fmt"
	"os/exec"
	"runtime"
	"strings"
)

type OpenTargetFolderStep struct {
	TargetFolder string
}

func (step OpenTargetFolderStep) Execute(executionContext base.ExecutionContext) int {
	fmt.Printf("Opening target folder %s \n", step.TargetFolder)
	openTargetFolder(step.TargetFolder)
	return 0
}

func (step OpenTargetFolderStep) StepData() base.StepData {
	return base.StepData{
		Name: "Open Target Step",
	}
}

func openTargetFolder(folder string) {

	cmd := "open"
	targetFolder := folder
	if runtime.GOOS == "windows" {
		cmd = "explorer"
		targetFolder = strings.ReplaceAll(folder, "/", "\\")
	}
	exec.Command(cmd, targetFolder).Start()
}
