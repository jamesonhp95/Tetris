   M S B A M L   `   `   `  ����A  =Tetris, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:TetrisTetris  5   	       Tetris.MainMenu   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Tetris     5   	     � Title$  Tetris Mania��5   	   $	��720��6   $	��650��6+    � 
ResizeMode$ NoResize=�67    � Icon$ TetrisManiaLogo.ico��6M   .��5	      � .+�5
   
   o� -   5         o�Name$ mainMenu��5
      $
��Left=�6!   $��30��6<   $	��Top=�6H   $	��873��6`   $��2��6l   .@�5      m� -   5         m�Name$ mnuFile��5      $��Center=�6(   $��Center=�6L   $��_File��6g   $��45��6v   $��96��6�   $��14(�6�   .@�5      m� -   5        $ mnuFileNewClassic��5      $��_Classic��66   $
��Left=�6H   $��45��6c   $	��140��6o   6{   m� 5      -   5        $ mnuFileNewGravity��5      $��_Cascade��66   $
��Left=�6H   $��45��6c   $	��140��6o   6{   5      m� 5      -   5        $ mnuHelp��5      $��_Help��6(   .@�5      m� -   5        $ mnuHelpAbout��5      $��_About��61   $
��Left=�6A   $��53��6\   $	��139��6h   6t   m� 5      -   5        $ mnuHelpDetails��5      $��_Details��63   $
��Left=�6E   $��53��6`   $	��139��6l   6x   5      5      �� 5   
   $)��#H:\Tetris\Tetris\TetrisLongLogo.png��6   $	��580��6=   $��0,111,321,0q�6J   6a   �� 5    
   -   5        $��Classic��5       $
��Left=�6#   $��	23,55,0,0q�6>   $	��Top=�6Q   $��85��6i   6t   �� 5!   
   -	   5        $��Cascade��5!      $
��Left=�6#   $��	23,80,0,0q�6>   $	��Top=�6Q   $��85��6i   6t   �� 5"   
   $[��UClassic - After completing rows, blocks above the row shift down that amount of rows.��6   $
��Left=�6p   $��
113,52,0,0q�6�   $	��Top=�6�   $	��521��6�   �� 5#   
   $`��ZCascade - After completing rows, blocks above the row shift down realistically to gravity.��6   $
��Left=�6u   $��
113,78,0,0q�6�   $	��Top=�6�   $	��521��6�   �� 5$   
   $��Player Name: ��6   $
��Left=�6(   $��	23,29,0,0q�6C   $	��Top=�6V   $��85��6n   �� 5%   
   -
   5         ��Name$ pName��5%      $
��Left=�6"   $��23��6=   $��
108,33,0,0q�6I    �� TextWrapping$
 Wrap=�6]    J� Opacity$ .5Q�6q   $@��:Type your name here. If left blank, defaults to Anonymous!��6~   $	��Top=�6�   $	��511��6�   6�   �� 5&   
   -   5         ��Name$ label��5&      $����Controls:

Left/Right arrow keys move left and
right respectively.

Up/Down arrow keys pivot the falling
pattern counterclockwise and 
clockwise respectively.

Space bar drops the block.

CTRL+P or P - Pause game
CTRL+G or G - Resume game
CTRL+X or X - Exit game
R - Replay game after losing

CTRL+S - Save game
CTRL+L - Load game






Secret: Click the Tetris Mania logo
for chaotic fun!!
(Increases level in real game based
on amount of times clicked)��6   $
��Left=�6�  $��375,144,0,0q�6�  $	��Top=�6  $	��466��6+  $	��219��68  �� 5'   
   -   5        	 ��Name$	 LogoLevelUp��5'      $�� ��6&   $ 0Q�61   $
��Left=�6=   $��
74,319,0,0q�6X   $	��Top=�6l   $	��159��6�   $��66��6�   
 K� ZIndex$	
 100��6�   5(      5)      