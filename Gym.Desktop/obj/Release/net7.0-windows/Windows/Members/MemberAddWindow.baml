   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem   Gym.Desktop1   +Gym.Desktop.Windows.Members.MemberAddWindow   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6sys&clr-namespace:System;assembly=mscorlib     -     � Title$  Add New Membership��$	��700��$	��500�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=� � WindowStyle$
 None=� � 	Resources    
TitleLabel �� 
TargetType �� Property  PrimaryTextColor �� Value��%�   &	        �� " ��.���  � ��# �  ��  � ��$ Cascadia Mono)���  � ��$ 16(���  � ��$ SemiBold"���  � ��$	 0 5q�.���   PrimaryDialogBackground#���  .+��  � RowDefinitions �� $��30 ��� $��* �.+�� $��0��  PrimaryBackground#���  	 � ColumnDefinitions	 �� $��* ��� $��30 �.+��� -   
 ��Name$
 brMemberAddDrager��$��0��#���   � Cursor$ 	ScrollAll���� $��1��#���  $��Right=�.+��� -    ��Name$ btnMemberAddExit��  
ExitButton#���  ��  �� Orientation$ Vertical=�$��20q�$��1��.+��� .+��� $��Package Types��#���   ��   PrimaryComboBoxBackground#���  $��30��._��� -    ��Name$ cmbPackageList��$��Cascadia Mono)�$��16(�$��SemiBold"�$
��10 5q� �� Text$ Please select ...�� �� 
IsEditable$
 True�� �� 
IsReadOnly$
 True��
������� �� DisplayMemberPath$ PackageName�� �� SelectedValuePath$ Id���� .+��� $��Member's Name��#���   �� #���  $��30��._��� -   $ cmbMembersList��$��Cascadia Mono)�$��16(�$��SemiBold"�$
��10 5q�$ Please select ...��$
 True��$
 True��
�������$ 	FirstName��$ Id���� .+��� $��Instructor's Name��#���   �� #���  $��30��._��� -   $ cmbInstructorsList��$��Cascadia Mono)�$��16(�$��SemiBold"�$
��10 5q�$ Please select ...��$
 True��$
 True��
�������$ 	FirstName��$ Id���� ._�� 	 �� $��* ��� $��15 ��� $��* �.+��� $��0��.+��� $��
Start Date��#���   ��   PrimaryTextBoxColor#���  $��40��._�(  "System.Windows.Controls.DatePicker  -     Name$ dtpStartDate��  FormDatePicker#���  �� $��1���� $��2��.+��� $��End Date��#���   $��Center=��� #���  $��40��._�  -   $ 
dtpEndDate��#���  �� ._�� 	 �� $��* ��� $��15 ��� $��* �.+��� $��0��.+��� $��Membership Status��#���   �� #���  $��40��._��� -	   $ cmbMembershipStatus��$��Cascadia Mono)�$��16(�$��SemiBold"�$
��10 5q�$ Please select ...��$
 True��$
 True��
�������#���  $ Content�� �� SelectedValue �� *VMselectedOption+ �� Mode$ TwoWay=�.@��� .��Active�� .��	Cancelled�� .��	Pending�� .��	Expired�� .��	Suspended�� .��	Defaulted�� $��1���� $��2��.+��� $��Payment Date��#���   $��Center=��� #���  $��40��._�  -
   $ dtpPaymentDate��#���  �� .+��� $��Payment Status��#���   � $	��200��$
��Left=�	 �� $��* ��� $��* �.+�	� -    	�Name$ rbPaid��$��0��$
��Paid��$��16(�$
 Hand��$��Center=�$��Center=�#���  	� $��1��$��Unpaid��$
 Hand�� d� 	IsChecked$
 TrueY�$��16(�$��Center=�$��Center=�#���  �� $��Description��#���   �� #���  $	��100��._��� -    ��Name$ rbDescription��$��10q�$��16(�$��Cascadia Mono)�$��0q�#���  #���   �/� T�J� 
�������.*�Type here something...�� -   $ btnSave��$
��Save��$��	0 20 0 10q�