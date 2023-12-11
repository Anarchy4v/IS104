Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports MySql.Data.MySqlClient

Public Class SalesWindow
    Private connectionString As String = "server=127.0.0.1;userid=root;password='';database=tgp_db"
    Public Shared salesWindowInstance As SalesWindow
    Private totalAmount As Decimal
    Private totalSalesPrice As Decimal

    'this is needed because it references to my ModalOrderSales2
    Public Class OrderDetails
        Public Property ItemName As String
        Public Property Quantity As Integer
        Public Property Dosage As String
        Public Property Price As Decimal
        Public Property SalesId As Integer
    End Class

    ' Add a method to retrieve and display compute_sales data
    Private Sub DisplayComputeSalesData()
        Try
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT sales_id, sales_name, sales_dosage, sales_qty, sales_price FROM compute_sales"

                Using cmd As New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        ' Assuming you have a DataGridView named dgvComputeSales in your form
                        DataGridView1.Rows.Clear()

                        ' Variables to store total sales price
                        Dim totalSalesPrice As Decimal = 0

                        While reader.Read()
                            DataGridView1.Rows.Add(
                                reader("sales_id"),
                                reader("sales_name"),
                                reader("sales_dosage"),
                                reader("sales_qty"),
                                reader("sales_price")
                            )

                            totalSalesPrice += Convert.ToDecimal(reader("sales_price"))
                        End While

                        ' Display the total sales price in Label8
                        Label8.Text = $"₱{totalSalesPrice:N2}"
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error retrieving compute_sales data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SalesWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Display Discountts
        ComboBox1.Items.Add("Regular Discount (0%)")
        ComboBox1.Items.Add("SENIOR / PWD DISCOUNT: 20%")
        ComboBox1.Items.Add("SENIOR / PWD WITH BOOK AND PRESCRIPTION: 32%")
        ComboBox1.SelectedIndex = 0

        If DataGridView1.Columns.Count = 0 Then
            DataGridView1.Columns.Add("SalesId", "Sales ID")
            DataGridView1.Columns.Add("SalesName", "Item Name")
            DataGridView1.Columns.Add("SalesDosage", "Item Dosage")
            DataGridView1.Columns.Add("ColumnSalesQty", "Item Quantity")
            DataGridView1.Columns.Add("ColumnSalesPrice", "Item Price")
        End If

        DisplayComputeSalesData()

        If Decimal.TryParse(Label8.Text.Replace("₱", "").Replace(",", "").Trim(), totalAmount) Then
            'shit
        Else
            MessageBox.Show("Invalid total amount format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ' Get the selected item from ComboBox1
        Dim selectedDiscountOption As String = ComboBox1.SelectedItem.ToString()

        ' Call ApplyDiscount based on the selected option
        If selectedDiscountOption = "SENIOR / PWD DISCOUNT: 20%" Then
            ApplyDiscount(0.2)
        ElseIf selectedDiscountOption = "SENIOR / PWD WITH BOOK AND PRESCRIPTION: 32%" Then
            ApplyDiscount(0.32)
        ElseIf selectedDiscountOption = "Regular Discount (0%)" Then
            ApplyDiscount(0)
        End If
    End Sub

    Private Sub ApplyDiscount(discount As Double)
        Dim discountedAmount As Decimal = totalAmount * (1 - CDec(discount))
        Label5.Text = $"{discountedAmount:N2}"
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        UpdateChangeLabel()
    End Sub

    Private Sub UpdateChangeLabel()
        If Decimal.TryParse(TextBox1.Text, Nothing) AndAlso Decimal.TryParse(Label8.Text.Replace("₱", "").Replace(",", "").Trim(), totalAmount) Then
            Dim discount As Double = 0
            Select Case ComboBox1.SelectedItem.ToString()
                Case "SENIOR / PWD DISCOUNT: 20%"
                    discount = 0.2
                Case "SENIOR / PWD WITH BOOK AND PRESCRIPTION: 32%"
                    discount = 0.32
                Case "Regular Discount (0%)"
                    discount = 0
            End Select

            Dim discountedAmount As Decimal = totalAmount * (1 - CDec(discount))
            Dim cashValue As Decimal = Decimal.Parse(TextBox1.Text)
            Dim result As Decimal = cashValue - discountedAmount

            If result < 0 Then
                result = 0
            End If

            Label6.Text = $"₱{result:N2}"
        Else
            Label6.Text = "Invalid input"
        End If
    End Sub
End Class
