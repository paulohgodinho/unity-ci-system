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

	fmt.Println("Select Platform:")
	for i, item := range platformList {
		fmt.Printf("%v - %s \n", i, item)
	}

	selectedPlatoformIndex := 0
	fmt.Scanln(&selectedPlatoformIndex)
	fmt.Printf("Selected %v \n", selectedPlatoformIndex)

	fmt.Println("Available Templates")
	var templateList []string = getAllTemplates()
	for i, item := range templateList {
		fmt.Printf("%v - %s \n", i, item)
	}

	selectedTemplateIndex := 0
	fmt.Println("Select Template:")
	fmt.Scanln(&selectedTemplateIndex)
	fmt.Printf("Selected Template: %v", selectedPlatoformIndex)

	unityPath, error := os.ReadFile("unitypath")
	if error != nil {
		log.Fatalln(error)
	}

	fmt.Printf("Using Unity from: %s \n", unityPath)

	args := fmt.Sprintf("-batchmode -logFIle - -quit -executeMethod CIBuildTool.DoBuild platform:%s template:%s",
		platformList[selectedPlatoformIndex],
		templateList[selectedTemplateIndex],
	)
	cmd := exec.Command(string(unityPath), strings.Split(args, " ")...)

	stderr, _ := cmd.StdoutPipe()
	cmd.Start()

	scanner := bufio.NewScanner(stderr)
	//scanner.Split(bufio.ScanWords)
	for scanner.Scan() {
		m := scanner.Text()
		fmt.Println(m)
	}
	fmt.Println("Waiting")
	cmd.Wait()
	fmt.Println("Finished Waiting")

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
