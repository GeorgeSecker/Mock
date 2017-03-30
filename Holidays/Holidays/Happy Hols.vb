Imports System.IO
'Imports System
Imports System.Text
Public Class Form1
    Dim ClientId As Integer = 0 'Declared globally because declaring it locally will cause the value to reset 

    Private Sub cmdCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheck.Click
        Dim Precheck As Boolean = False 'Will be used to check if the textboxes pass the validation test

        If txtEmail.Text = "" Then
            MessageBox.Show("A textbox has been left empty.", "WARNING", MessageBoxButtons.OK)
            Precheck = False
        ElseIf txtFirstName.Text = "" Then
            MessageBox.Show("A textbox has been left empty.", "WARNING", MessageBoxButtons.OK)
            Precheck = False
        ElseIf txtLastName.Text = "" Then
            MessageBox.Show("A textbox has been left empty.", "WARNING", MessageBoxButtons.OK) 'Will warn the user if a textbox has been left empty and will prevent the data from getting loaded
            Precheck = False
        Else : Precheck = True 'Will allow the data to be appended into txtStoreDetails
        End If


        If Precheck = True Then
            ClientId = ClientId + 1 'Makes the client id increase with each customer
            txtStoreDetails.AppendText(vbTab & "  Client ID:  " & ClientId & vbNewLine) 'will load all the textboxes data into txtStoreDetails which allows me to format the data through this textbox before it gets saved
            txtStoreDetails.AppendText("" + vbNewLine)
            txtStoreDetails.AppendText(vbTab & lblFirstName.Text + "  :  " + txtFirstName.Text + vbNewLine) 'vbtab and vbnewline is used to make it formatted better when it is saved to the text file
            txtStoreDetails.AppendText("" + vbNewLine) 'Creates a empty line between the data
            txtStoreDetails.AppendText(vbTab & lblLastName.Text + "  :  " + txtLastName.Text + vbNewLine)
            txtStoreDetails.AppendText("" + vbNewLine)
            txtStoreDetails.AppendText(vbTab & lblDOB.Text + "  :  " + dtpDOB.Value + vbNewLine)
            txtStoreDetails.AppendText("" + vbNewLine)
            txtStoreDetails.AppendText(vbTab & lblEmail.Text + "  :  " + txtEmail.Text + vbNewLine)
            txtStoreDetails.AppendText("" + vbNewLine)
        End If
    End Sub
    Private Sub txtFirstName_KeyPress(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txtFirstName.KeyDown, txtLastName.KeyDown 'Adding both of the textboxes to Handles... will not require a seperate sub for each textboxes

        If (e.KeyCode >= Keys.D0 And e.KeyCode <= Keys.D9) Then
        ElseIf (e.KeyCode >= Keys.NumPad0 And e.KeyCode <= Keys.NumPad9) Then 'This will prevent all number keys including the numpad from being entered into the name textboxes
            ' This will prevent users from typing in numbers and will automatically send a backspace to remove any numbers
            SendKeys.Send("{BackSpace}")
        End If

    End Sub

    Private Sub cmdClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        Dim Check As Integer = MessageBox.Show("Are you sure you want to clear all your inputted data? This action will not erase saved data.", "WARNING", MessageBoxButtons.YesNo)

        If Check = DialogResult.Yes Then
            txtStoreDetails.Text = "" ' This will give the user a message to confirm if they want it to be deleted or not and if they select yes, the data stored in txtStoreDetails will be cleared
        End If
    End Sub
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim Path As String = "c:\vb\customerdetails.txt" 'Name of the text file
        Dim Direct As String = "c:\vb\" 'path that will be used to store the txt file



        If File.Exists(Path) = True Then
            Dim Details As String = txtStoreDetails.Text
            File.AppendAllText(Path, Details) ' Will add data stored in the txtStoreDetails to the txt file

        Else

            If Not Directory.Exists(Direct) Then
                Directory.CreateDirectory(Direct) ' This will create the path in which the txt file will be stored in
                Dim TextFile As System.IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter("c:\vb\customerdetails.txt", True) 'will create the textfile as the previous line can only create the directory
                TextFile.Close() 'Must close this afterwards otherwise the program will crash when trying to save details
                MessageBox.Show("Directory created. Please reclick save to save your data", "", MessageBoxButtons.OK)
            End If
        End If
    End Sub
    Private Sub cmdLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoad.Click

        txtStoreDetails.Text = System.IO.File.ReadAllText("c:\vb\customerdetails.txt") 'Will load the txt file to the textbox


        If txtStoreDetails.Text = "" Then
            MessageBox.Show("No Data Was Found", "Error", MessageBoxButtons.OK) 'Will display an error message saying that there is no data in the file
        End If

    End Sub
    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        Dim Path As String = "c:\vb\customerdetails.txt" 'The path that is used to find the txt file
        Dim Check As Integer = MessageBox.Show("Do you want to erase all stored records?", "WARNING", MessageBoxButtons.YesNo) 'Needs to be declared to use DialogResult

        If Check = DialogResult.Yes Then 'Will give the user a chance to cancel the operation if they accidently click the button
            System.IO.File.WriteAllText(Path, "") 'This will erase everything in the txt file be overwriting it with a a null character
        End If


    End Sub
End Class