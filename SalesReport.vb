Public Class SalesReport
    ' Add a DataTable field to store order details
    Private _orderDetails As DataTable

    ' Constructor that accepts order details
    Public Sub New(orderDetails As DataTable)
        InitializeComponent()
        _orderDetails = orderDetails
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'dashboard
        Dim dashForm As New Dash()
        dashForm.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'point of sales
        Dim sales As New Sales()
        sales.Show()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'inventory
        Dim inv As New Inventory()
        inv.Show()
        Me.Close()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Me.Close()
            Form1.Show()
        End If
    End Sub

    Public WriteOnly Property OrderDetails As DataTable
        Set(value As DataTable)
            ' Clear existing columns and rows before setting a new data source
            DataGridView1.DataSource = Nothing
            _orderDetails = value

            ' Check if order details are available
            If _orderDetails IsNot Nothing Then
                ' Set the DataGridView data source to the order details DataTable
                DataGridView1.DataSource = _orderDetails
            End If
        End Set
    End Property

    Private Sub SalesReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Check if order details are available
        If _orderDetails IsNot Nothing Then
            ' Set the DataGridView data source to the order details DataTable
            DataGridView1.DataSource = _orderDetails
        End If
    End Sub
End Class