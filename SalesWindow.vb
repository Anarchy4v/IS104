Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class SalesWindow
    Private _totalPrice As Decimal
    Private outCash As Decimal
    Private totalAmount As Decimal
    ' Reference to the Sales form
    Private salesForm As Sales

    ' Constructor to receive a reference to the Sales form
    Public Sub New(salesForm As Sales)
        InitializeComponent()
        Me.salesForm = salesForm
    End Sub

    Public Property TotalPrice As Decimal
        Get
            Return _totalPrice
        End Get
        Set(value As Decimal)
            _totalPrice = value
            UpdateTotalPriceLabel()
        End Set
    End Property

    Private Sub SalesWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopulateDiscountOptions()
    End Sub

    Private Sub PopulateDiscountOptions()
        ' Populate ComboBox1 with discount options
        ComboBox1.Items.Add("20% - SENIOR / PWD DISCOUNT")
        ComboBox1.Items.Add("32% - SENIOR / PWD WITH BOOK AND PRESCRIPTION")
    End Sub

    Private Sub UpdateTotalPriceLabel()
        ' Update Label8 with the total price
        Label8.Text = $"₱{_totalPrice:F2}"
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ' Apply the selected discount to the total price
        Select Case ComboBox1.SelectedIndex
            Case 0
                ApplyDiscount(0.2)
            Case 1
                ApplyDiscount(0.32)
        End Select
    End Sub

    Private Sub ApplyDiscount(discountRate As Decimal)
        Dim discountedPrice As Decimal = TotalPrice * (1 - discountRate)
        Label5.Text = $"₱{discountedPrice:F2} ({(discountRate * 100):F0}%)"
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        ' Remove non-numeric characters from TextBox1
        Dim cleanedInput As String = New String(TextBox1.Text.Where(Function(c) Char.IsDigit(c)).ToArray())
        TextBox1.Text = cleanedInput
        TextBox1.SelectionStart = TextBox1.Text.Length
        CalculateChange()
    End Sub

    Private Sub CalculateChange()
        If Decimal.TryParse(TextBox1.Text, outCash) Then
            Dim discountedPriceText As String = Label5.Text.Substring(1, Label5.Text.IndexOf(" "c) - 1)

            If Decimal.TryParse(discountedPriceText, totalAmount) Then
                If totalAmount >= outCash Then
                    Dim change As Decimal = totalAmount - outCash
                    Label6.Text = $"₱{change:F2}"
                Else
                    Label6.Text = "Insufficient Cash"
                End If
            Else
                ' Handle the case where parsing discounted price fails
                Label6.Text = "Invalid Total Amount"
            End If
        Else
            Label6.Text = "Invalid Input"
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to submit the transaction?", "Submit Transaction", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            ' Assuming you have a function to retrieve order details, replace it with the actual code
            Dim orderDetails As DataTable = GetOrderDetails()

            ' Open SalesReport form and pass the order details
            Dim salesReportForm As New SalesReport(orderDetails)
            salesReportForm.Show()

            ' Close the SalesWindow form
            Me.Close()
        End If
    End Sub

    ' Add a function to retrieve order details (replace this with your actual implementation)
    Private Function GetOrderDetails() As DataTable
        ' Replace this with the actual code to retrieve order details from your data source
        Dim orderDetails As New DataTable()

        ' Assuming you have some logic to populate orderDetails DataTable
        ' For example:
        orderDetails.Columns.Add("Product", GetType(String))
        orderDetails.Columns.Add("Quantity", GetType(Integer))
        ' Add rows with product and quantity data

        Return orderDetails
    End Function

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub
End Class