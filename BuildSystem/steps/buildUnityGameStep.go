package steps

import (
	"bufio"
	"buildsystem/base"
	"fmt"
	"log"
	"os"
	"os/exec"
	"path/filepath"
	"strings"
)

type BuildUnityGameStep struct {
}

func (step BuildUnityGameStep) Execute(executionContext base.ExecutionContext) int {

	platformList := []string{
		"Windows",
		"Mac",
		"Linux",
	}

	base.Printl("Select Platform:")
	for i, item := range platformList {
		base.Printl(fmt.Sprintf("%v - %s", i, item))
	}

	selectedPlatoformIndex := 0
	fmt.Scanln(&selectedPlatoformIndex)
	base.Printl(fmt.Sprintf("Selected %v", selectedPlatoformIndex))

	base.Printl("Available Templates")
	var templateList []string = getAllTemplates()
	for i, item := range templateList {
		base.Printl(fmt.Sprintf("%v - %s", i, item))
	}

	selectedTemplateIndex := 0
	base.Printl("Select Template:")
	fmt.Scanln(&selectedTemplateIndex)
	base.Printl(fmt.Sprintf("Selected Template: %v", selectedPlatoformIndex))

	unityPath, error := os.ReadFile("unitypath")
	if error != nil {
		log.Fatalln(error)
	}

	base.Printl(fmt.Sprintf("Using Unity from: %s", unityPath))

	args := fmt.Sprintf("-batchmode -logFIle - -quit -executeMethod CIBuildTool.DoBuild platform:%s template:%s",
		platformList[selectedPlatoformIndex],
		templateList[selectedTemplateIndex],
	)
	cmd := exec.Command(string(unityPath), strings.Split(args, " ")...)

	stderr, _ := cmd.StdoutPipe()
	cmd.Start()

	scanner := bufio.NewScanner(stderr)
	for scanner.Scan() {
		m := scanner.Text()
		base.Printl(m)
	}

	cmd.Wait()

	return 0
}

func (step BuildUnityGameStep) StepData() base.StepData {
	return base.StepData{
		Name: "Build Unity Game",
	}
}

func getAllTemplates() []string {
	files, err := os.ReadDir("../Assets/CI/Templates")
	if err != nil {
		log.Fatal(err)
	}

	var templateList []string
	for _, file := range files {
		if filepath.Ext(file.Name()) == ".asset" {
			templateList = append(templateList, file.Name())
		}
	}

	return templateList
}
