   M S B A M L   `   `   `  ����L  HProjet_Sauvegarde, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Projet_SauvegardeProjet_Sauvegarde  5   	   "    Projet_Sauvegarde.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Projet_Sauvegarde     5   	   -   5          �Name$  rootWindows��5   	    � 
ResizeMode$ NoResize=�5	   	    � Title$ 
MainWindow��5
   	   $	��585��6   $
��1316��6)   .��5      � $��0,0,10,8q�6   .+�5   
   	� -   5         	�Name$ CompleteRadio��5         complete#���   6-   $
��Left=�6^   $��	40,29,0,0q�6y   $	��Top=�6�   6�   	� 5   
   -   5        $ 	DiffRadio��5        differential#���  6)   $
��Left=�6^   $��
152,29,0,0q�6y   $	��Top=�6�   6�   �� 5   
   -   5         ��Name$ QuitAppButton��5        quit#���  6(   $
��Left=�6U   $��
24,517,0,0q�6p   $	��Top=�6�    J� RenderTransformOrigin$ 0.232,0.616&�6�   $��50��6�    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      5      �� 5   
   -   5         ��Name$ TextNameOfSave��5      $
��Left=�6*   $��
40,114,0,0q�6E    �� TextWrapping$ NoWrap=�6Y   $	��Top=�6o   $	��151��6�   $��22��6�   �� 5   
   -   5        $ TextSourcePath��5      $
��Left=�6*   $��
40,172,0,0q�6E   $ NoWrap=�6Y   $	��Top=�6o   $	��151��6�   $��22��6�   �� 5   
     enterNameBackup#���  6   $
��Left=�6H   $��	19,90,0,0q�6c   $	��Top=�6v   �� 5   
     enterSourceBackup#���  6   $
��Left=�6J   $��
19,144,0,0q�6e   $	��Top=�6y   �� 5   
     enterDestinationBackup#���  6   $
��Left=�6O   $��
19,200,0,0q�6j   $	��Top=�6~   �� 5   
   -   5        $ TextDestinationPath��5      $
��Left=�6/   $��
40,230,0,0q�6J   $ NoWrap=�6^   $	��Top=�6t   $	��151��6�   $��23��6�   �� 5   
   -   5        $ AddBackupButton��5        	addBackup#���  6*   $
��Left=�6\   $��
40,327,0,0q�6w   $	��Top=�6�   $	��113��6�   
������� 5        5      5      �� 5   
   -	   5        $ StartSaveButton��5        buttonStart#���  6*   $
��Left=�6^   $��1214,517,0,0q�6y   $	��Top=�6�   ���	#FF88E41A 5        5       5"      �� 5$   
   -
   5        $ DeleteConfigButton��5$        deleteConfig#���  6-   $
��Left=�6b   $��372,400,0,0q�6}   $	��Top=�6�   
������� 5%        5&      5(      �� 5)   
    	 pathCompleteBackup#��� 	 6   $
��Left=�6K   $��
14,260,0,0q�6f   $	��Top=�6z   $ 	0.6,0.352&�6�   �� 5*   
   -   5        $ TextLastComplete��5*      $
��Left=�6,   $��
40,289,0,0q�6G   $ NoWrap=�6[   $	��Top=�6q   $	��151��6�   $��23��6�   �� 5+   
    
 typeSave#��� 
 6   $
��Left=�6A   $��14,0,0,0q�6\   $	��Top=�6n   $
��Bold"�6�   $��14(�6�   �� 5,   
     
backupInfo#���  6   $
��Left=�6C   $��	14,68,0,0q�6^   $	��Top=�6q   $
��Bold"�6�   $��14(�6�   �� 5-   
     allConfigBackup#���  6   $
��Left=�6H   $��	334,9,0,0q�6c   $	��Top=�6v   $
��Bold"�6�   �� 5.   
     backupToBeLaunched#���  6   $
��Left=�6K   $��
821,14,0,0q�6f   $	��Top=�6z   $
��Bold"�6�   �� 5/   
   -   5        $ TakeConfigButton��5/        takeConfigButton#���  6+   $
��Left=�6d   $��674,400,0,0q�6   $	��Top=�6�   
������� 50        51      53      �� 54   
   $��334,41,520,170q�6   $	��350��6/   .��55      &   System.Windows.Controls.DataGrid  -   5        	  Name$	 AllConfigList��55      
   AutoGenerateColumns$
 False��6/      
IsReadOnly$
 True��6K   ��6]   ��  �� Path$ AllConfigBackup�   Columns 56      0  *System.Windows.Controls.DataGridTextColumn  57      ,  &System.Windows.Controls.DataGridColumn   Header$
 Type��6C      Width5  /System.Windows.Controls.DataGridLengthConverter$ 70 6Q   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   �� *Type+6Q     58      $
 Name��6C   $ * 6Q    6)   �� *Name+6Q     59      $ Source��6H   $ 2* 6X    6)   �� *
SourcePath+6X     5:      $ Destination��6M   $ 2* 6b    6)   �� *DestinationPath+6b     5;      $ Complete��6N   $ * 6`    6)   �� *CompleteSavePath+6`   5=      5>      �� 5?   
   -   5         ��Name$ BackupLaunch��5?      $��824,41,32,170q�6-   $	��350��6D   .��5@        -   5        $	 BackupListLaunch��5@      $
 False��62   $
 True��6N   ��6`   �� *AllBackupLaunch+ 5A        5B      $
 Type��6C   $ 70 6Q    6)   �� *Type+6Q     5C      $
 Name��6C   $ * 6Q    6)   �� *Name+6Q     5D      $ Source��6H   $ 2* 6X    6)   �� *
SourcePath+6X     5E      $ Destination��6M   $ 2* 6b    6)   �� *DestinationPath+6b     5F      $ Complete��6N   $ * 6`    6)   �� *CompleteSavePath+6`     5G      $ Progression��6I   $ * 6^    6)   �� *Progression+6^   5I      5J      �� 5K   
   -   5        $ DeleteQueueButton��5K        deleteQueueButton#���  6,   $
��Left=�6f   $��835,400,0,0q�6�   $	��Top=�6�   
�������
�������
���   � 5L        5M      5O      �� 5P   
   -   5        $ EnglishButton��5P      $
��Left=�6(   $��116,521,0,0q�6C   $	��Top=�6X   $��18��6p   $��12��6�   $ 0.561,0.559&�6�   ���	#FFDDDDDD.��5Q      �� $��/Pictures/engflag.jpg��6   $��Uniform=�63   5R      �� 5T   
   -   5        $ FranchButton��5T      $
��Left=�6(   $��165,521,0,0q�6C   $	��Top=�6X   $��19��6p   $��12��6{   .��5U      �� $��/Pictures/frflag.jpg��6   $��Uniform=�62   5V      �� 5W   
     enterExtension#���  6   $
��Left=�6G   $��821,425,0,0q�6b   $	��Top=�6w   �� 5X   
   -   5        $ TextExtEncrypt��5X      $
��Left=�6*   $��835,451,0,0q�6E   $ NoWrap=�6Z   $	��Top=�6p   $	��120��6�   �� 5Y   
     enterSoftware#���  6   $
��Left=�6F   $��821,469,0,0q�6a   $	��Top=�6v   �� 5Z   
   -   5        $ ExecutableText��5Z      $
��Left=�6*   $��835,495,0,0q�6E   $ NoWrap=�6Z   $	��Top=�6p   $	��120��6�   �� 5[   
   -   5        $ SaveExtension��5[        save#���  6(   $
��Left=�6U   $��974,451,0,0q�6p   $	��Top=�6�    5\        5]      5_      �� 5`   
   -   5        $ SaveProcess��5`      #���  6&   $
��Left=�6S   $��974,495,0,0q�6n   $	��Top=�6�    5a        5b      5d      �� 5e   
     extensionActive#���  6   $
��Left=�6H   $��1050,448,0,0q�6c   $	��Top=�6y   �� 5f   
     processActive#���  6   $
��Left=�6F   $��1050,491,0,0q�6a   $	��Top=�6w   �� 5g   
   $
��Left=�62   $��1150,448,0,0q�6M   $	��Top=�6c   ��6   �� *ExtensionSave+6c   �� 5h   
   $
��Left=�60   $��1150,491,0,0q�6K   $	��Top=�6a   ��6   �� *ProcessSave+6a   �� 5i   
   -   5        $ Pause��5i      $��Pause��6    $
��Left=�60   $��945,404,0,0q�6K   $	��Top=�6`   6x   �� 5j   
   -   5        $
 Stop��5j      $
��Stop��6   $
��Left=�6.   $��1038,404,0,0q�6I   $	��Top=�6_   6w   �� 5k   
   -   5        $ TallText��5k      $
��Left=�6$   $��419,500,0,0q�6?   $ NoWrap=�6T   $	��Top=�6j   $	��120��6�   �� 5l   
   -   5        $ SaveTall��5l      #���  6#   $
��Left=�6P   $��558,500,0,0q�6k   $	��Top=�6�    5m        5n      5p      �� 5q   
     
tallActive#���  6   $
��Left=�6C   $��635,496,0,0q�6^   $	��Top=�6s   �� 5r   
   $
��Left=�6-   $��715,497,0,0q�6H   $	��Top=�6]   ��6   �� *
TallSave+6]   5u      5v      