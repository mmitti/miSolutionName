# miSolutionName

Modify solution name ui in title bar for Visual Studio 2019.  
Visual Studio2019の右上に表示されるソリューション名のUIをカスタマイズします。

![.](./doc/example.png)

# Feature/機能

* Change solution name ui background/foreground color.  
  右上に表示されるソリューション名のUIの配色を変更します。
* Specify different colors when window active and inactive.  
  ウィンドウのアクティブ状態に応じて異なる配色を指定できます。
* Add solution name ui to sub window.  
  メインウィンドウ以外のウィンドウにもソリューション名のUIを追加します。
* Work with Visual Studio Code
  This extension can load VSCode "workbench.colorCustomizations".  
  Visual Studio Codeの設定ファイルの配色設定を読み込みます。
* Automaticaly colors each solution like[WindowColors](https://github.com/stuartcrobinson/unique-window-colors) [WIP]

# Usage/使い方
## DefaultColor/デフォルト配色

You can set colors in Visual Studio Options. This setting will apply to any  
solution except enable .suo specific option.  
If you want to change colors, Open [Tools]>[Options]>[Environment]>[miSolutionName].  

VisualStudioのオプションでデフォルトの配色を設定できます。この設定は.suo固有の  
オプションが無効のソリューションに適用されます。  
変更するには「ツール」>「オプション」>「環境」>「miSolutionName」を開きます。  

## .suo-Specific Option/.suo固有の設定

Solution User Option(.suo) specific setting.
If Enabled, you can change colors per solution.  
If you want to set colors, Open Editor [Extensions]>[miSolutionName Setting] and,  
(1) Check [Enable .suo Options]  
(2) Fill colors text box(hex color format).  
Those settings will saved to .suo file. So you have to set per solution.  

ソリューションユーザーオプション固有の設定を変更できます。  
この設定を有効にするとソリューションごとに配色を指定することができます。  
有効にするには「拡張機能」>「miSolutionName設定」でエディターを開き、  
(1) 「.suoに格納されている設定を有効にする」にチェックを入れ  
(2) 配色内のテキストボックスにカラーコードを入力してください。  
これらの設定は.suoファイルに保存されるため、ソリューションごとに設定する必要があります。  

## Load VS Code Seting/VS Codeの設定を読み込む

This Extension can load color customization from VS Code settings.json or .code-workspace.
If you want to load VS Code customization,  [Extensions]>[miSolutionName Setting] and,  
(1) Check [Enable .suo Options]  
(2) Check [Find and load VS Code settings or workspace]  
Those settings will saved to .suo file. So you have to set per solution.  

この拡張機能はVS Codeの設定ファイル(setting.json)やワークスペースファイル(*.code-workspace)
に保存されている配色設定を読み込むことができます。
有効にするには「拡張機能」>「miSolutionName設定」でエディターを開き、  
(1) 「.suoに格納されている設定を有効にする」にチェックを入れ  
(2) 「VSCodeの設定やワークスペースを探索し、設定ファイルを読み込みます」にチェックを入れます。
これらの設定は.suoファイルに保存されるため、ソリューションごとに設定する必要があります。  

### VS Code config search rule/VS Codeの設定ファイルの探索ルール

This extension find VS Code configure file in those rules.  
(1) Move to parent directory of current soluton file.  
(2) If *.code-workspace is exists and contains color customization, load workspace setting.  
(3) If  .vscode/settings.json is exists and contains color customization, load setting.json.  
(4) Otherwize, Move to parent directory, and try (2)~(4).

VS Codeの設定ファイルは以下のルールで探索します。  
(1) 現在開いているソリューションファイルの親フォルダーに移動します。  
(2) *.code-workspaceが存在し、配色設定が存在すればそれを読み込みます。  
(3) .vscode/settings.jsonが存在し、配色設定が存在すればそれを読み込みます。  
(4) さらに一つ親のフォルダーに移動し、(2)~(4)を繰り返します。

### Supported VS Code setting keys/読み込み可能なVS Codeの設定項目

```json
{
	"workbench.colorCustomizations": {
		"titleBar.activeBackground": "#004488",
        "titleBar.activeForeground": "#FFFFFF",
        "titleBar.inactiveBackground": "#909090",
		"titleBar.inactiveForeground": "#000000"
	}
}
```

# Misc
Konyanyatiwa. This is a Japasene. Kore, nihongo yanen.
Eigo kakuno mendoi nen. Nihongo de yurusitena.
Kore, "Kaisya" de hukusuu no branch de sagyou siteiruto window no kubetu ga
tukanakunari, komarunode tukuttan. VSSolutionColor Extension ga Visual Studio 2019
de umaku ugokanen, nande kore tukutta. Ato, onazi project de VSCode mo tukaunen,
Window Colors Extension de branch gotoni window no iro kaetenen.
Nande kore tukuttan. Hona sainara.

# Related Projects/関連するプロジェクト
* https://github.com/stuartcrobinson/unique-window-colors  
Visual Studio Code Extension. 
* https://github.com/mayerwin/vs-customize-window-title/  
Visual Studio Extension. 
* https://github.com/Wumpf/VSSolutionColor  
Visual Studio Extension. 

# History
* v1.1  
Add VS Code Settings loader.  
Load "workbench.colorCustomizations" from setting.json/.code-workspace .

* v1.0  
First release.
