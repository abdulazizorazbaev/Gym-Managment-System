   M S B A M L   `   `   `  ����   Gym.Desktop    Gym.Desktop.MainWindow   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     @d2http://schemas.microsoft.com/expression/blend/2008     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     � Title$  
MainWindow�� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�$	��650��$
��1000��
������  � AllowsTransparency$
 True��.���  � RowDefinitions �� $��30 ��� $��* �.+���    PrimaryBackground#���   $��0��  PrimaryBorderColor#���  $��0 0 0 1q�._�� $��10 0 0 0q� � ColumnDefinitions �� $��* ��� $	��105 �.+��� -    ��Name$ brDrager��$��0��#���    � Cursor$ 	ScrollAll���� $��1��#���   	 �� Orientation$	 
Horizontal=�.+��� -   
 ��Name$
 btnMinimize��  MinimizeButton#���  �� -   $
 btnMaximize��  MaximizeButton#���  �� -   $
 btnExit��  
ExitButton#���  � $��1�� �� $	��250 ��� $��* �.+��� $��0��#���   ._�� $��0 20 0 0q�.+��  �� $��* ��� $��40 �.+��� $��0��.+��� $��60��$��	0 20 0 20q�$��Center=�._��� $!��Assets/Images/logo-icon.png��	� -    	�Name$ rbDashboard��$��	Dashboard�� d� 	IsChecked$
 TrueY�  
MenuButton#���    dashboard-icon
 � Tag# �  	� -   $ 
rbPackages��$��Packages��#���    package-icon# �  	� -   $ 	rbClients��$��Clients��#���    	user-icon# �  	� -	   $ rbInstructors��$��Instructors��#���   	 instructor-icon# � 	 	� -
   $ 	rbMembers��$��Memberships��#���   
 member-icon# � 
 	� -   $ 
rbPayments��$��Payments��#���    payment-icon# �  	� -   $ rbFAQ��$	��FAQ��#���    faq-icon# �  	� -   $ rbAbout��$��About��#���    
about-icon# �  �� $��1��$
��Left=�$��20 0 0 0q�._��� -    ��Name$ switcherTheme��$��Center=�$��Center=�  SwitchThemeButton#���  ��   SecondaryBackground#���  $��1��._� � -     �Name$ PageNavigator��  � NavigationUIVisibility$ Hidden=�