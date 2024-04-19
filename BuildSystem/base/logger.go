package base

import (
	"fmt"

	"github.com/charmbracelet/lipgloss"
)

func PrintWelcome() {
	text := "Welcome to the CI System"

	var style = lipgloss.NewStyle().
		BorderStyle(lipgloss.NormalBorder()).
		BorderForeground(lipgloss.Color("63")).
		Width(100).
		Align(lipgloss.Center)

	fmt.Println(style.Render(text))
}

func PrintStepText(text string) {
	var style = lipgloss.NewStyle().
		Bold(true).
		Foreground(lipgloss.Color("#FAFAFA")).
		Background(lipgloss.Color("#7D56F4")).
		PaddingLeft(1).
		Width(100).SetString("> ").
		Bold(true)

	fmt.Println(style.Render(text))
}

func PrintDelimiter() {
	var style = lipgloss.NewStyle().
		Bold(true).
		Background(lipgloss.Color("#9879F6")).
		PaddingLeft(2).
		Width(100).SetString("").
		Bold(true)

	fmt.Println(style.Render(""))
}

func Printl(text string) {
	PrintInsideStepText(text)
}

func PrintInsideStepText(text string) {
	var style = lipgloss.NewStyle().
		PaddingLeft(2).
		Width(100).SetString("-- ").
		Bold(true)

	fmt.Println(style.Render(text))
}
