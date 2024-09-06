# Programming-2B-POE


<Window x:Class="CMCS.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Contract Monthly Claim System" Height="450" Width="800">
    <Grid>
        <!-- Define the main layout with two columns: Navigation Panel and Main Content Area -->
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="81"/>
            <ColumnDefinition Width="47"/>
            <ColumnDefinition Width="596"/>
            <ColumnDefinition Width="76"/>
            <ColumnDefinition Width="0*"/>
        </Grid.ColumnDefinitions>

        <!-- Navigation Panel -->
        <StackPanel Background="#2C3E50" Grid.Column="1" Grid.ColumnSpan="2" Margin="4,0,2,0">
            <TextBlock Text="CMCS" FontSize="24" Foreground="White" HorizontalAlignment="Center" Margin="10"/>
            
            <Button Content="Lecturer" Name="LecturerButton" Margin="10" Padding="10" Click="LecturerButton_Click"/>
            <Button Content="Programme Coordinator" Name="CoordinatorButton" Margin="10" Padding="10" Click="CoordinatorButton_Click"/>
            <Button Content="Academic Manager" Name="ManagerButton" Margin="10" Padding="10" Click="ManagerButton_Click"/>
            <Button Content="Logout" Name="LogoutButton" Margin="10" Padding="10" VerticalAlignment="Bottom"/>
        </StackPanel>

        <!-- Main Content Area -->
        <Grid Grid.Column="4" Name="MainContentArea" Margin="38,0,-38,86">
            <!-- Lecturer View -->
            <StackPanel Name="LecturerView" Visibility="Collapsed">
                <TextBlock Text="Lecturer Dashboard" FontSize="20" Margin="10"/>
                <Button Content="Submit New Claim" Name="SubmitClaimButton" Margin="10" Padding="10"/>
                <Button Content="Upload Supporting Documents" Name="UploadDocumentsButton" Margin="10" Padding="10"/>
                <Button Content="Track Claim Status" Name="TrackStatusButton" Margin="10" Padding="10"/>
            </StackPanel>

            <!-- Programme Coordinator View -->
            <StackPanel Name="CoordinatorView" Visibility="Collapsed">
                <TextBlock Text="Programme Coordinator Dashboard" FontSize="20" Margin="10"/>
                <Button Content="View Pending Claims" Name="ViewPendingClaimsButton" Margin="10" Padding="10"/>
                <Button Content="Approve/Reject Claims" Name="ApproveRejectClaimsButton" Margin="10" Padding="10"/>
            </StackPanel>

            <!-- Academic Manager View -->
            <StackPanel Name="ManagerView" Visibility="Collapsed">
                <TextBlock Text="Academic Manager Dashboard" FontSize="20" Margin="10"/>
                <Button Content="View Approved Claims" Name="ViewApprovedClaimsButton" Margin="10" Padding="10"/>
                <Button Content="Approve/Reject Claims" Name="ManagerApproveRejectClaimsButton" Margin="10" Padding="10"/>
            </StackPanel>
        </Grid>
    </Grid>
</Window>










<Window x:Class="CMCS.Lecturer"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:CMCS"
        mc:Ignorable="d"
        Title="Lecturer" Height="450" Width="800">
    <Grid Height="NaN" Margin="0,24,0,0" Width="NaN">

        <StackPanel Background="#2C3E50" Margin="4,-20,2,0">
            <!-- Heading -->
            <TextBlock Text="Lecturer Dashboard" 
               FontSize="28" 
               FontWeight="Bold" 
               Foreground="White"
               HorizontalAlignment="Center" 
               Margin="0,0,0,20"/>
        </StackPanel>


        <Button Content="Submit Claim" Name="SubmitClaimButton" Margin="292,70,280,272" Padding="10" Click="SubmitClaimButton_Click"/>
        <Button Content="Upload Documents" Name="UploadDocumentsButton" Margin="292,171,280,171" Padding="10" Click="UploadDocumentsButton_Click"/>
        <Button Content="Check Status" Name="CheckStatusButton" Margin="292,275,280,67" Padding="10" Click="CheckStatusButton_Click"/>




    </Grid>
</Window>








<Window x:Class="CMCS.AcademicManager"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:CMCS"
        mc:Ignorable="d"
        Title="AcademicManager" Height="450" Width="800">
  

       


        <Grid Height="NaN" Margin="0,24,0,0" Width="NaN">

            <StackPanel Background="#2C3E50" Margin="4,-20,2,0">
                <!-- Heading -->
                <TextBlock Text="Academic Manager" 
    FontSize="28" 
    FontWeight="Bold" 
    Foreground="White"
    HorizontalAlignment="Center" 
    Margin="0,0,0,20"/>
            </StackPanel>


            <Button Content="Verify and Approve Claim" Name="VerifyClaimButton" Margin="273,93,249,191" Padding="10" Click="VerifyClaimButton_Click"/>




        </Grid>
</Window>








<Window x:Class="CMCS.ProgrammeCoordinator"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:CMCS"
        mc:Ignorable="d"
        Title="ProgrammeCoordinator" Height="450" Width="800">
    

        <Grid Height="NaN" Margin="0,24,0,0" Width="NaN">

            <StackPanel Background="#2C3E50" Margin="4,-20,2,0">
                <!-- Heading -->
                <TextBlock Text="Programme Coordinator" 
           FontSize="28" 
           FontWeight="Bold" 
           Foreground="White"
           HorizontalAlignment="Center" 
           Margin="0,0,0,20"/>
            </StackPanel>


        <Button Content="Verify and Approve Claim" Name="VerifyClaimButton" Margin="273,93,249,191" Padding="10" Click="VerifyClaimButton_Click"/>
          











        </Grid>
</Window>




