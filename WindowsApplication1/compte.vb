Imports System.IO
Public Class compte

    Private Sub compte_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Si le repertoire du jeu n'est pas créé
        If Not Directory.Exists(chemin) Then
            Directory.CreateDirectory(chemin)
        End If

        My.Computer.FileSystem.CurrentDirectory = chemin
        ' On créé une liste de tous les comptes existants
        For Each compteTrouvé As String In My.Computer.FileSystem.GetDirectories(chemin, FileIO.SearchOption.SearchTopLevelOnly)
            compteTrouvé = Strings.Replace(compteTrouvé, chemin, "")
            ComboBox1.Items.Add(compteTrouvé)
            ComboBox1.SelectedIndex = 0
        Next
    End Sub

    ' Si l'on a voulu créer un compte
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' Si il existe déja
        If My.Computer.FileSystem.DirectoryExists(TextBox1.Text) Then
            ErrorProvider1.SetError(TextBox1, "Ce compte existe déja, veuillez changez de nom")
        Else
            ' Sinon on créé le dossier
            Directory.CreateDirectory(chemin + "\" + TextBox1.Text + "\")
            ' On créé les fichiers nécessaires
            FileOpen(1, chemin + "\" + TextBox1.Text + "\nbr_partie.ini", OpenMode.Output)
            Print(1, "1" & vbCrLf)
            FileClose(1)
            FileOpen(1, chemin + "\" + TextBox1.Text + "\stats.ini", OpenMode.Output)
            Print(1, "0" & vbCrLf)
            Print(1, "0" & vbCrLf)
            Print(1, "0" & vbCrLf)
            Print(1, "0" & vbCrLf)
            FileClose(1)
            ' on sauvegarde le nom de l'utilisateur
            utilisateur = TextBox1.Text
            accueil.Show()
            Me.Visible = False
        End If

    End Sub
    ' Si on veut accéder à un ancien compte
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        If Not My.Computer.FileSystem.DirectoryExists(ComboBox1.SelectedItem.ToString) Then
            ErrorProvider1.SetError(ComboBox1, "Il n'existe aucun compte à ce nom")
        Else
            utilisateur = ComboBox1.SelectedItem.ToString
            accueil.Show()
            Me.Visible = False
        End If

    End Sub
End Class