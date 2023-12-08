Public Class ModalOrderSales2
    ' Property to set the user ID
    Private _userId As Integer

    Public Property UserId As Integer
        Get
            Return _userId
        End Get
        Set(value As Integer)
            _userId = value
        End Set
    End Property

    Public ReadOnly Property OrderQuantity As Integer
        Get
            Dim quantity As Integer
            If Integer.TryParse(TextBox1.Text, quantity) Then
                Return quantity
            Else
                Return 0 ' Default value or handle it differently based on your requirements
            End If
        End Get
    End Property

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Add any additional logic you need when the OK button is clicked
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
End Class
