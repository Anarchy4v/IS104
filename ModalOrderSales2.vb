Imports PharmacyandMedicine.SalesWindow

Public Class ModalOrderSales2
    Public Property OrderDetails As OrderDetails

    ' Add parameters to the constructor
    Public Sub New(itemName As String, initialQuantity As Integer)
        InitializeComponent()
        OrderDetails = New OrderDetails()

        ' Set initial values
        OrderDetails.ItemName = itemName
        OrderDetails.Quantity = initialQuantity
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub ModalOrderSales2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Display initial values
        TextBox1.Text = OrderDetails.Quantity.ToString()
    End Sub

    Private Sub ButtonSubmit_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Validate and get the quantity input
        If Integer.TryParse(TextBox1.Text, Me.OrderDetails.Quantity) Then
            ' Set the price based on your logic (e.g., quantity * item_price)
            ' For now, let's assume a fixed price of 10. Adjust as needed.
            Me.OrderDetails.Price = Me.OrderDetails.Quantity * 2

            ' Show a dialog indicating that the medicine has been added successfully
            MessageBox.Show("Medicine added successfully to Ordering Details.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Close the ModalOrderSales2 form
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Else
            MessageBox.Show("Invalid quantity. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class
