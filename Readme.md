# miSolutionName

Modify solution name ui in title bar for Visual Studio 2019.  
Visual Studio2019�̉E��ɕ\�������\�����[�V��������UI���J�X�^�}�C�Y���܂��B

![.](./doc/example.png)

# Feature/�@�\

* Change solution name ui background/foreground color.  
  �E��ɕ\�������\�����[�V��������UI�̔z�F��ύX���܂��B
* Specify different colors when window active and inactive.  
  �E�B���h�E�̃A�N�e�B�u��Ԃɉ����ĈقȂ�z�F���w��ł��܂��B
* Add solution name ui to sub window.  
  ���C���E�B���h�E�ȊO�̃E�B���h�E�ɂ��\�����[�V��������UI��ǉ����܂��B
* Work with Visual Studio Code
  This extension can load VSCode "workbench.colorCustomizations".  
  Visual Studio Code�̐ݒ�t�@�C���̔z�F�ݒ��ǂݍ��݂܂��B
* Automaticaly colors each solution like[WindowColors](https://github.com/stuartcrobinson/unique-window-colors) [WIP]

# Usage/�g����
## DefaultColor/�f�t�H���g�z�F

You can set colors in Visual Studio Options. This setting will apply to any  
solution except enable .suo specific option.  
If you want to change colors, Open [Tools]>[Options]>[Environment]>[miSolutionName].  

VisualStudio�̃I�v�V�����Ńf�t�H���g�̔z�F��ݒ�ł��܂��B���̐ݒ��.suo�ŗL��  
�I�v�V�����������̃\�����[�V�����ɓK�p����܂��B  
�ύX����ɂ́u�c�[���v>�u�I�v�V�����v>�u���v>�umiSolutionName�v���J���܂��B  

## .suo-Specific Option/.suo�ŗL�̐ݒ�

Solution User Option(.suo) specific setting.
If Enabled, you can change colors per solution.  
If you want to set colors, Open Editor [Extensions]>[miSolutionName Setting] and,  
(1) Check [Enable .suo Options]  
(2) Fill colors text box(hex color format).  
Those settings will saved to .suo file. So you have to set per solution.  

�\�����[�V�������[�U�[�I�v�V�����ŗL�̐ݒ��ύX�ł��܂��B  
���̐ݒ��L���ɂ���ƃ\�����[�V�������Ƃɔz�F���w�肷�邱�Ƃ��ł��܂��B  
�L���ɂ���ɂ́u�g���@�\�v>�umiSolutionName�ݒ�v�ŃG�f�B�^�[���J���A  
(1) �u.suo�Ɋi�[����Ă���ݒ��L���ɂ���v�Ƀ`�F�b�N�����  
(2) �z�F���̃e�L�X�g�{�b�N�X�ɃJ���[�R�[�h����͂��Ă��������B  
�����̐ݒ��.suo�t�@�C���ɕۑ�����邽�߁A�\�����[�V�������Ƃɐݒ肷��K�v������܂��B  

## Load VS Code Seting/VS Code�̐ݒ��ǂݍ���

This Extension can load color customization from VS Code settings.json or .code-workspace.
If you want to load VS Code customization,  [Extensions]>[miSolutionName Setting] and,  
(1) Check [Enable .suo Options]  
(2) Check [Find and load VS Code settings or workspace]  
Those settings will saved to .suo file. So you have to set per solution.  

���̊g���@�\��VS Code�̐ݒ�t�@�C��(setting.json)�⃏�[�N�X�y�[�X�t�@�C��(*.code-workspace)
�ɕۑ�����Ă���z�F�ݒ��ǂݍ��ނ��Ƃ��ł��܂��B
�L���ɂ���ɂ́u�g���@�\�v>�umiSolutionName�ݒ�v�ŃG�f�B�^�[���J���A  
(1) �u.suo�Ɋi�[����Ă���ݒ��L���ɂ���v�Ƀ`�F�b�N�����  
(2) �uVSCode�̐ݒ�⃏�[�N�X�y�[�X��T�����A�ݒ�t�@�C����ǂݍ��݂܂��v�Ƀ`�F�b�N�����܂��B
�����̐ݒ��.suo�t�@�C���ɕۑ�����邽�߁A�\�����[�V�������Ƃɐݒ肷��K�v������܂��B  

### VS Code config search rule/VS Code�̐ݒ�t�@�C���̒T�����[��

This extension find VS Code configure file in those rules.  
(1) Move to parent directory of current soluton file.  
(2) If *.code-workspace is exists and contains color customization, load workspace setting.  
(3) If  .vscode/settings.json is exists and contains color customization, load setting.json.  
(4) Otherwize, Move to parent directory, and try (2)~(4).

VS Code�̐ݒ�t�@�C���͈ȉ��̃��[���ŒT�����܂��B  
(1) ���݊J���Ă���\�����[�V�����t�@�C���̐e�t�H���_�[�Ɉړ����܂��B  
(2) *.code-workspace�����݂��A�z�F�ݒ肪���݂���΂����ǂݍ��݂܂��B  
(3) .vscode/settings.json�����݂��A�z�F�ݒ肪���݂���΂����ǂݍ��݂܂��B  
(4) ����Ɉ�e�̃t�H���_�[�Ɉړ����A(2)~(4)���J��Ԃ��܂��B

### Supported VS Code setting keys/�ǂݍ��݉\��VS Code�̐ݒ荀��

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

# Related Projects/�֘A����v���W�F�N�g
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
