Imports System.Xml
Imports System.IO
Imports System.Security.Cryptography

Public Class accueil

    Private Sub accueil_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Initialisation()
    End Sub

    Public Sub Initialisation()
        ' On décharge le listview
        ListView1.Clear()
        ' Création des colonnes
        ListView1.View = View.Details
        ListView1.Columns.Add(New ColumnHeader)
        ListView1.Columns(0).Text = "ID"
        ListView1.Columns(0).Width = 40
        ListView1.Columns.Add(New ColumnHeader)
        ListView1.Columns(1).Text = "Date de création"
        ListView1.Columns(1).Width = 130
        ListView1.Columns.Add(New ColumnHeader)
        ListView1.Columns(2).Text = "Date de dernière utilisation"
        ListView1.Columns(2).Width = 130
        ListView1.Columns.Add(New ColumnHeader)
        ListView1.Columns(3).Text = "Niveau"
        ListView1.Columns(3).Width = 50
        ListView1.Columns.Add(New ColumnHeader)
        ListView1.Columns(4).Text = "Tirs restants"
        ListView1.Columns(4).Width = 90

        Dim cheminUtilisateur As String = chemin & utilisateur + "\"
        ' on cherche toutes les parties existantes
        For Each partieTrouvée As String In My.Computer.FileSystem.GetFiles(cheminUtilisateur, FileIO.SearchOption.SearchTopLevelOnly, "*.ini")

            Dim information As FileInfo = My.Computer.FileSystem.GetFileInfo(partieTrouvée)

            partieTrouvée = Strings.Replace(partieTrouvée, ".ini", "")
            partieTrouvée = Strings.Replace(partieTrouvée, cheminUtilisateur, "")
            If partieTrouvée <> "nbr_partie" And partieTrouvée <> "stats" Then
                ' Format du nom de fichier : date création, date derniere utilisation, niveau, coups restants
                Dim DonneesParties() As String = Split(partieTrouvée, "_")


                Dim ListView As ListViewItem = New ListViewItem(DonneesParties(0))

                ListView.SubItems.Add(information.CreationTime)
                ListView.SubItems.Add(information.LastWriteTime)
                ListView.SubItems.Add(DonneesParties(1))
                ListView.SubItems.Add(DonneesParties(2))
                ' on l'ajoute au control
                Me.ListView1.Items.Add(ListView)
            End If
        Next
        ' Création des statistiques
        Statistiques()
    End Sub

    Private Sub Statistiques()
        My.Computer.FileSystem.CurrentDirectory = chemin & utilisateur + "\"

        FileOpen(1, "stats.ini", OpenMode.Input)

        Input(1, TextNbGagnées.Text)
        Input(1, TextNbPerdues.Text)
        Input(1, TextPlusHautNiveau.Text)
        Input(1, TextJeuTerminé.Text)

        FileClose(1)
    End Sub
    ' Si l'on veut commencer une nouvelle partie
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        exPartie = 0
        nomExPartie = ""
        niveau = NumericUpDown1.Value
        jeu.ShowDialog()
    End Sub
    ' Si l'on veut reprendre une partie
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim indexs As ListView.SelectedIndexCollection = ListView1.SelectedIndices
        'on recupere l'id et le nom du fichier de la partie
        For Each index As Integer In indexs
            exPartie = ListView1.Items(index).SubItems(0).Text
            nomExPartie = ListView1.Items(index).SubItems(0).Text + "_" + ListView1.Items(index).SubItems(3).Text + "_" + ListView1.Items(index).SubItems(4).Text + ".ini"
        Next

        jeu.ShowDialog()
    End Sub
    ' Si l'on a sélectionné une ancienne partie
    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        Button2.Enabled = True
    End Sub
    ' si l'on veut changer de compte
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        compte.Visible = True
    End Sub
    ' si on ferme cette fenetre on ferme tout le programme
    Private Sub Me_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.FormClosed
        compte.Close()
    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

    End Sub
End Class