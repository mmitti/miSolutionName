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
* Work with [Window Colors(for VSCode)](https://github.com/stuartcrobinson/unique-window-colors)  
  This extension can load Window Colors config file(WIP).

# Usage/�g����
* DefaultColor/�f�t�H���g�z�F  
  You can set colors in Visual Studio Options. This setting will apply to any  
  solution except enable .suo specific option.  
  If you want to change colors, Open [Tools]>[Options]>[Environment]>[miSolutionName].  
  
  VisualStudio�̃I�v�V�����Ńf�t�H���g�̔z�F��ݒ�ł��܂��B���̐ݒ��.suo�ŗL��  
  �I�v�V�����������̃\�����[�V�����ɓK�p����܂��B  
  �ύX����ɂ́u�c�[���v>�u�I�v�V�����v>�u���v>�umiSolutionName�v���J���܂��B  
  
* .suo-Specific Option/.suo�ŗL�̐ݒ�
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
