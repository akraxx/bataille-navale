Imports System.Drawing
Imports System.IO

Public Class jeu
    Dim g As Graphics
    Dim PtCoin As System.Drawing.Point = New System.Drawing.Point(30, 30)
    Dim AfficheSeulementTirs As Boolean = True 'pour débuguer on peut afficher les bateaux :-)
    Dim Gagné As Boolean = False
    Dim Perdu As Boolean = False

    'Taille de la mer et contenu des cases
    Const LargeurMer As Integer = 10    'en fait la mer carrée ..
    Const HauteurMer As Integer = 10
    Const Rien As Integer = 0           'pas de tir effectué à cet endroit
    Const BatTouché As Integer = 1
    Const BatSurf As Integer = 2        'il y a un bateau et il n'a pas été touché à cet endroit
    Const TirALeau As Integer = 3       'un tir à été effectué à cet endroit

    Dim Mer(LargeurMer, HauteurMer) As Integer  'le tableau à 2 dimensions "mer(,) permet d'enregistrer les tirs

    Dim tirRestant As Integer

    'Codes tirs
    Const Aleau As Integer = 0
    Const Touché As Integer = 1         ' le dernier tir a touché le bateau
    Const Coulé As Integer = 2          ' le dernier tir a coulé le bateau
    Const ToutCoulé As Integer = 3      'tous les bateaux ont été coulés après le dernier tir
    Const PlusDeTir As Integer = -1     'impossible d'effectuer d'autres tirs

    'Bateaux
    Const NbBateaux As Integer = 6
    Structure Point
        Public X As Integer
        Public Y As Integer
    End Structure

    Structure Bateau
        Public Type As String
        Public Taille As Integer
        Public Position As Point
        Public Orientation As Integer '0 = Gauche à droite , 1 = Haut en Bas
        Public Flotte As Boolean 'Pas encore coulé
    End Structure

    Dim Flotte(6) As Bateau

    Public Sub InitFlotte(ByRef Flotte() As Bateau)
        Flotte(1).Type = "Porte-Avion"
        Flotte(1).Taille = 5
        Flotte(1).Flotte = False    'le bateau n'est pas encore placé, il n'existe donc pas

        Flotte(2).Type = "Destroyer"
        Flotte(2).Taille = 4
        Flotte(2).Flotte = False

        Flotte(3).Type = "Frégate"
        Flotte(3).Taille = 3
        Flotte(3).Flotte = False

        Flotte(4).Type = "Frégate"
        Flotte(4).Taille = 3
        Flotte(4).Flotte = False

        Flotte(5).Type = "Sous-Marin"
        Flotte(5).Taille = 2
        Flotte(5).Flotte = False

        Flotte(6).Type = "Sous-Marin"
        Flotte(6).Taille = 2
        Flotte(6).Flotte = False

    End Sub




    Private Sub jeu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' si ce n'est pas une ancienne partie on intilise tout
        If exPartie = 0 Then
            InitialisationPartie()
            ' sinon on remet les parametres de l'ancienne partie
        Else
            ReinitialisationPartie()
        End If
    End Sub

    Private Sub InitialisationPartie()
        Me.Width = 800
        Me.Height = 400
        'Lors du Chargement du formulaire...
        g = Me.CreateGraphics
        'Niveau du jeu
        TextNiveau.Text = "Niveau " & CStr(niveau)

        tirRestant = NbTirNiv1 - (niveau * coupsEnMoinsParNiveau)
        ' réinitilisation des valeurs pour un nouveau niveau
        Button3.Visible = False
        Button2.Enabled = True
        Gagné = False
        Perdu = False
        TextResultat.Text = ""

        TextRestant.Text = tirRestant
        'Initialisation du générateur de nombres alétoires 
        Randomize()
        'initialisation de la grille de tir avec des zéros
        InitTab(Mer, Rien)
        ' génération de la composition de la flotte dans le tableau flotte()
        InitFlotte(Flotte)
        ' placement des bateaux sur la mer
        PlacerBateaux(Mer, Flotte)
    End Sub

    Public Sub ReinitialisationPartie()
        My.Computer.FileSystem.CurrentDirectory = chemin & utilisateur + "\"
        Me.Width = 800
        Me.Height = 400
        'Lors du Chargement du formulaire...
        g = Me.CreateGraphics

        ' Réinitialisation des valeurs
        Button3.Visible = False
        Button2.Enabled = True
        Gagné = False
        Perdu = False
        TextResultat.Text = ""

        'on ouvre le fichier
        FileOpen(1, nomExPartie, OpenMode.Input)
        Do While Not EOF(1)
            ' On lit chaque ligne
            Dim valeurs() As String = Split(LineInput(1), "=")
            If valeurs(0) = "tirRestant" Then
                tirRestant = CInt(valeurs(1))
                TextRestant.Text = tirRestant
            ElseIf valeurs(0) = "niveau" Then
                niveau = CInt(valeurs(1))
                TextNiveau.Text = "Niveau " & CStr(niveau)
            Else
                Dim valeurs_details() As String = Split(valeurs(0), "-")

                Dim i As Integer = CInt(valeurs_details(1))
                If valeurs_details(0) = "Flotte" Then
                    If valeurs_details(2) = "Position" Then
                        If valeurs_details(3) = "X" Then
                            Flotte(i).Position.X = valeurs(1)
                        Else
                            Flotte(i).Position.Y = valeurs(1)
                        End If
                    ElseIf valeurs_details(2) = "Taille" Then
                        Flotte(i).Taille = valeurs(1)
                    Else
                        Flotte(i).Flotte = CBool(valeurs(1))
                    End If
                Else
                    Dim positions() As String = Split(valeurs(1), "-")
                    For j As Integer = 1 To LargeurMer
                        Mer(i, j) = CInt(positions(j - 1))
                    Next
                End If
            End If
        Loop

        FileClose(1)
    End Sub

    Private Sub PlacerBateaux(ByVal Mer(,) As Integer, ByRef Flotte() As Bateau)

        For i As Integer = 1 To NbBateaux
            Dim BateauPlacé As Boolean = False
            Dim Orientation As Integer
            Dim Position As Point
            'tant que le bateau n'est aps bien placé
            While Not BateauPlacé
                ' on dit que le bateau est bien placé tant qu'on a pas trouvé d'erreur
                BateauPlacé = True
                Orientation = OrientationAleatoire()

                If Orientation = 0 Then ' De gauche à droite
                    ' on cherche une position aléatoire
                    Position = PositionAleatoire(LargeurMer - Flotte(i).Taille + 1, HauteurMer)
                    ' on fais une boucle de la positionX trouvée jusqu'a la fin du bateau
                    For j As Integer = Position.X To Position.X + Flotte(i).Taille - 1
                        Dim Pt As Point
                        Pt.X = j
                        Pt.Y = Position.Y
                        'Si l'on trouve un bateau on remet le bateauplacé à false
                        If PresenceBateau(Mer, Pt) Then
                            BateauPlacé = False
                        End If
                    Next
                    ' on fait de même si le bateau est de haut en bas
                Else
                    Position = PositionAleatoire(LargeurMer, HauteurMer - Flotte(i).Taille + 1)

                    For j As Integer = Position.Y To Position.Y + Flotte(i).Taille - 1
                        Dim Pt As Point
                        Pt.X = Position.X
                        Pt.Y = j
                        If PresenceBateau(Mer, Pt) Then
                            BateauPlacé = False
                        End If
                    Next
                End If

                'Si le bateau a été placé
                If BateauPlacé Then
                    ' On initilise la flotte
                    Flotte(i).Flotte = True
                    Flotte(i).Position = Position
                    Flotte(i).Orientation = Orientation
                    ' on le rajoute à la grille
                    If Orientation = 0 Then
                        For j As Integer = Position.X To Position.X + Flotte(i).Taille - 1
                            Mer(j, Position.Y) = BatSurf
                        Next
                    Else
                        For j As Integer = Position.Y To Position.Y + Flotte(i).Taille - 1
                            Mer(Position.X, j) = BatSurf
                        Next
                    End If
                End If
            End While
        Next

    End Sub

    Private Function Tir(ByRef mer(,) As Integer, ByRef flotte() As Bateau, ByVal Coordonnées As Point) As Integer
        ' cette fonction réalise un tir dans le tableau mer(,) et y inscrit le type d'impact. Elle décompte un tir du total permis
        '
        ' si la position sur la mer contient une valeur  differente de la valeur "rien", on a touché un bateau non coulé ou déjà touché
        ' marquage du tir
        ' recherche du bateau touché dans flotte() et renvoyer le code correspondant à "touché"
        ' si bateau coulé, le faire disparaitre de la surface de l'eau 
        ' si bateau coulé, le marquer "coulé" et renvoyer le code correspondant
        ' regarder si tous les bateaux ont été coulés et renvoyer le code correspondant
        Dim resultat As Integer

        If mer(Coordonnées.X, Coordonnées.Y) <> Rien And mer(Coordonnées.X, Coordonnées.Y) <> TirALeau Then
            mer(Coordonnées.X, Coordonnées.Y) = BatTouché

            ' On dit que le bateau est coulé tant que l'on a pas trouvé une case qui n'est pas touchée
            Dim FlotteCoulé As Boolean = True
            Dim BateauCoulé As Boolean = True

            'Selection de la flotte
            For i As Integer = 1 To NbBateaux
                'position de gauche à droite
                If flotte(i).Orientation = 0 Then
                    If (flotte(i).Position.X <= Coordonnées.X And flotte(i).Position.X + flotte(i).Taille - 1 >= Coordonnées.X) And flotte(i).Position.Y = Coordonnées.Y Then
                        ' boucle allant de la position du bateau jusqu'a sa fin
                        For j As Integer = flotte(i).Position.X To flotte(i).Position.X + flotte(i).Taille - 1
                            ' si l'on trouve une case non touché on dit que le bateau n'est aps encore coulé
                            If mer(j, flotte(i).Position.Y) <> BatTouché Then
                                BateauCoulé = False
                            End If
                        Next
                        ' Si le bateau est coulé, on le supprime
                        If BateauCoulé Then
                            flotte(i).Flotte = False
                        End If
                    End If
                    ' on fait la même chose pour l'orientation verticale
                Else
                    If (flotte(i).Position.Y <= Coordonnées.Y And flotte(i).Position.Y + flotte(i).Taille - 1 >= Coordonnées.Y) And flotte(i).Position.X = Coordonnées.X Then


                        For j As Integer = flotte(i).Position.Y To flotte(i).Position.Y + flotte(i).Taille - 1
                            If mer(flotte(i).Position.X, j) <> BatTouché Then
                                BateauCoulé = False
                            End If
                        Next
                        ' Si le bateau est coulé, on le supprime
                        If BateauCoulé Then
                            flotte(i).Flotte = False
                        End If
                    End If
                End If

                'Si un bateau n'est pas coulé, la flotte n'est aps entierement coulé
                If flotte(i).Flotte Then
                    FlotteCoulé = False
                End If
            Next

            If FlotteCoulé Then
                resultat = ToutCoulé
            ElseIf BateauCoulé Then
                resultat = Coulé
            ElseIf Not BateauCoulé Then
                resultat = Touché
            End If
        Else
            resultat = Aleau
            mer(Coordonnées.X, Coordonnées.Y) = TirALeau
        End If

        ' Si l'on a plus de tir, mais que tout est coulé
        If tirRestant = 1 And resultat <> ToutCoulé Then
            Return PlusDeTir
        Else
            tirRestant -= 1
            Return resultat
        End If
    End Function

    Private Function PresenceBateau(ByVal Mer(,) As Integer, ByVal Coordonnées As Point) As Boolean
        ' regarde s'il y a bateau sur la mer à cet endroit
        If Mer(Coordonnées.X, Coordonnées.Y) = BatSurf Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function PositionAleatoire(ByVal LongueurMax As Integer, ByVal HauteurMax As Integer) As Point
        ' renvoie une postion aléatoire pour un bateau , comprise entre (1, 1) et (10,10)
        Dim pointAleatoire As Point
        pointAleatoire.X = CInt(Int((LongueurMax * Rnd()) + 1))
        pointAleatoire.Y = CInt(Int((HauteurMax * Rnd()) + 1))

        Return pointAleatoire
    End Function

    Public Function OrientationAleatoire() As Integer
        'renvoie une orientation aléatoire 0 : horizontale 1 : verticale.
        Return CInt((1 * Rnd()) + 0)
    End Function


    Private Sub jeu_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        ' procédure à priori complète, affiche la grille et les tirs efectués
        grille(g, LargeurMer + 1)
        AffichageTir(g, Mer, AfficheSeulementTirs)

    End Sub
    Private Sub AffichageTir(ByRef g As System.Drawing.Graphics, ByVal Mer(,) As Integer, ByVal AfficheSeulementTirs As Boolean)
        'parcours le tableau mer(,) et affiche une pastille de couleur différente suivant ce qui se trouve dans la case
        For i As Integer = 1 To HauteurMer
            For j As Integer = 1 To LargeurMer
                Dim Brush As Brush
                If Mer(i, j) = BatTouché Then
                    Brush = New SolidBrush(Color.Red)
                ElseIf Mer(i, j) = TirALeau Then
                    Brush = New SolidBrush(Color.Yellow)
                ElseIf Mer(i, j) = Rien Then
                    Brush = New SolidBrush(Color.Blue)
                Else
                    If Not AfficheSeulementTirs Then
                        Brush = New SolidBrush(Color.Black)
                    Else
                        Brush = New SolidBrush(Color.Blue)
                    End If
                End If
                Dim PtPastille As System.Drawing.Point = New System.Drawing.Point(PtCoin.X + 30 * j + 3, PtCoin.Y + 30 * i + 3)
                Pastille(g, PtPastille, Brush)
            Next
            ' affichage des entetes
            g.DrawString(CStr(i), Label1.Font, Brushes.DarkBlue, PtCoin.X + 30 * i + 3, PtCoin.Y)
            g.DrawString(CStr(i), Label1.Font, Brushes.DarkGreen, PtCoin.X, PtCoin.Y + 30 * i + 3)
        Next
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' action déclenchée lors d'un click sur le bouton tir
        ' rien à ajouter à priori
        envoi_tir(TextLigne.Value, TextColonne.Value)
    End Sub

    Private Sub envoi_tir(ByVal pointX As Integer, ByVal pointY As Integer)
        My.Computer.FileSystem.CurrentDirectory = chemin & utilisateur + "\"

        Dim codeRetour As Integer
        Dim pt As Point
        pt.Y = pointY
        pt.X = pointX
        If Not Perdu And Not Perdu Then
            If pt.Y >= 1 And pt.Y <= HauteurMer And pt.X >= 1 And pt.X <= LargeurMer Then
                codeRetour = Tir(Mer, Flotte, pt)
                If codeRetour = Touché Then
                    TextResultat.Text = "Touché"
                ElseIf codeRetour = Aleau Then
                    TextResultat.Text = "A l'eau"
                ElseIf codeRetour = Coulé Then
                    TextResultat.Text = "Coulé"
                ElseIf codeRetour = ToutCoulé Then
                    TextResultat.Text = "Flotte entièrement coulée, bravo !"
                    Gagné = True
                    MAJstats("niveauGagné")
                    MAJstats("plushautniveau")

                    If niveau < niveau_max Then
                        Button3.Visible = True
                    Else
                        MAJstats("jeuTerminé")
                        TextResultat.Text = "Bravo, vous avez terminé tous les niveaux du jeu !"

                        If exPartie <> 0 Then
                            File.Delete(nomExPartie)
                        End If
                    End If

                    Button2.Enabled = False
                    
                ElseIf codeRetour = PlusDeTir Then
                    Button2.Enabled = False
                    TextResultat.Text = "Plus de munition - Perdu"
                    AfficheSeulementTirs = False
                    Perdu = True
                    ' si la parti est perdu on met à jour les statistiques et on supprime la partie
                    MAJstats("partiePerdue")
                    If exPartie <> 0 Then
                        File.Delete(nomExPartie)
                    End If
                End If
                TextRestant.Text = tirRestant
                AffichageTir(g, Mer, AfficheSeulementTirs)
            Else
                TextResultat.Text = "Tir hors limites, essai nul"
            End If
        Else
            TextResultat.Text = "Vous avez perdu, retentez votre chance"
            Button2.Enabled = False
        End If
    End Sub
    Public Sub InitTab(ByRef tableau(,) As Integer, ByVal DefaultValue As Integer)
        ' remplissage d un tableau à deux dimensions avec la valeur DefaultValue

        For i As Integer = 1 To HauteurMer
            For j As Integer = 1 To LargeurMer
                tableau(i, j) = DefaultValue
            Next
        Next

    End Sub

    Public Sub grille(ByRef g As System.Drawing.Graphics, ByVal taille As Integer)
        ' dessine une grille carrée representant la mer
        For i As Integer = 1 To taille
            g.DrawLine(Pens.Black, PtCoin.X + 30, PtCoin.Y + 30 * i, PtCoin.X + (30 * taille), PtCoin.Y + 30 * i)
            g.DrawLine(Pens.Black, PtCoin.X + 30 * i, PtCoin.Y + 30, PtCoin.X + 30 * i, PtCoin.Y + (30 * taille))
        Next
    End Sub

    Public Sub Pastille(ByRef g As System.Drawing.Graphics, ByVal Position As System.Drawing.Point, ByVal Couleur As System.Drawing.Brush)
        Dim Rect As System.Drawing.Rectangle = New System.Drawing.Rectangle(Position.X, Position.Y, 24, 24)
        g.FillEllipse(Couleur, Rect)
    End Sub

    Private Sub SauverPartie()
        My.Computer.FileSystem.CurrentDirectory = chemin & utilisateur + "\"
        ' Si c'est une ancienne partie
        If exPartie <> 0 Then
            ' on renomme le fichier
            Dim nouveauNom As String = CStr(exPartie) + "_" + CStr(niveau) + "_" + CStr(tirRestant) + ".ini"
            My.Computer.FileSystem.RenameFile(nomExPartie, nouveauNom)

            FileOpen(1, nouveauNom, OpenMode.Output)
        Else
            'sinon on cherche le denier id de partie
            FileOpen(1, "nbr_partie.ini", OpenMode.Input)
            Dim id_partie As Integer
            Do While Not EOF(1)
                ' Read line into variable.
                id_partie = CStr(LineInput(1))
            Loop
            FileClose(1)

            TextResultat.Text = id_partie

            FileOpen(1, "nbr_partie.ini", OpenMode.Output)
            Print(1, id_partie + 1 & vbCrLf)
            FileClose(1)
            ' on créé le fichier
            FileOpen(1, CStr(id_partie) + "_" + CStr(niveau) + "_" + CStr(tirRestant) + ".ini", OpenMode.Output)
        End If
        ' insertion de toutes les valeurs nécessaires pour la réouverture de la partie
        Print(1, "tirRestant=" & tirRestant & vbCrLf)
        Print(1, "niveau=" & niveau & vbCrLf)
        For i As Integer = 1 To NbBateaux
            Print(1, "Flotte-" + CStr(i) + "-Position-X=" + CStr(Flotte(i).Position.X) & vbCrLf)
            Print(1, "Flotte-" + CStr(i) + "-Position-Y=" + CStr(Flotte(i).Position.Y) & vbCrLf)
            Print(1, "Flotte-" + CStr(i) + "-Taille=" + CStr(Flotte(i).Taille) & vbCrLf)
            Print(1, "Flotte-" + CStr(i) + "-Flotte=" + CStr(Flotte(i).Flotte) & vbCrLf)

        Next

        For i As Integer = 1 To LargeurMer
            Print(1, "Mer-" + CStr(i) + "=")
            For j As Integer = 1 To HauteurMer
                If j = HauteurMer Then
                    Print(1, "-" + CStr(Mer(i, j)) & vbCrLf)
                ElseIf j = 1 Then
                    Print(1, CStr(Mer(i, j)))
                Else
                    Print(1, "-" + CStr(Mer(i, j)))
                End If
            Next
        Next

        FileClose()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        SauverPartie()
        TextResultat.Text = "Partie sauvegardée"
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        niveau += 1
        InitialisationPartie()
    End Sub

    Private Sub panel1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        Dim case1 As Point
        Dim case2 As Point
        ' Si l'on se trouve dans le cadre
        If (e.X > PtCoin.X And e.X < PtCoin.X + 30 * 11) And (e.Y > PtCoin.Y And e.Y < PtCoin.Y + 30 * 11) Then
            For i As Integer = 1 To HauteurMer
                For j As Integer = 1 To LargeurMer
                    ' Si l'on est dans une case, on créé un rectange aqua et on défnit le rectangle suivant et le rectange du dessous à ne pas modifier
                    If (e.X > PtCoin.X + j * 30 And e.X < PtCoin.X + (j + 1) * 30) And (e.Y > PtCoin.Y + i * 30 And e.Y < PtCoin.Y + (i + 1) * 30) Then
                        Dim rectange As Rectangle = New Rectangle(PtCoin.X + 30 * j, PtCoin.Y + 30 * i, 30, 30)
                        g.DrawRectangle(Pens.Aqua, rectange)
                        TextColonne.Value = j
                        TextLigne.Value = i

                        If j < 10 Then
                            case1.X = (j + 1)
                            case1.Y = i
                        End If

                        If i < 10 Then
                            case2.X = j
                            case2.Y = (i + 1)
                        End If
                    ElseIf (j <> case1.X And i <> case1.Y) And (j <> case2.X And i <> case2.Y) Then
                        Dim rectange As Rectangle = New Rectangle(PtCoin.X + 30 * j, PtCoin.Y + 30 * i, 30, 30)
                        g.DrawRectangle(Pens.Black, rectange)
                    End If
                Next
            Next
            Me.Cursor = Cursors.Cross
        Else
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub panel1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick
        'Si l'on clic dans une case de la grille
        If (e.X > PtCoin.X And e.X < PtCoin.X + 30 * 11) And (e.Y > PtCoin.Y And e.Y < PtCoin.Y + 30 * 11) Then
            For i As Integer = 1 To HauteurMer
                For j As Integer = 1 To LargeurMer
                    If (e.X > PtCoin.X + j * 30 And e.X < PtCoin.X + (j + 1) * 30) And (e.Y > PtCoin.Y + i * 30 And e.Y < PtCoin.Y + (i + 1) * 30) Then
                        envoi_tir(i, j)
                    End If
                Next
            Next
        End If
    End Sub

    Private Sub MAJstats(ByVal type As String)
        My.Computer.FileSystem.CurrentDirectory = chemin & utilisateur + "\"

        Dim nbrParGagnées As Integer
        Dim nbrParPerdues As Integer
        Dim plusHautniveau As Integer
        Dim nbrJeuTerminé As Integer

        FileOpen(1, "stats.ini", OpenMode.Input)
        ' sauvegarde des valeurs
        Input(1, nbrParGagnées)
        Input(1, nbrParPerdues)
        Input(1, plusHautniveau)
        Input(1, nbrJeuTerminé)

        FileClose(1)
        ' switch selon les cas
        If type = "niveauGagné" Then
            nbrParGagnées += 1
        ElseIf type = "partiePerdue" Then
            nbrParPerdues += 1
        ElseIf type = "jeuTerminé" Then
            nbrJeuTerminé += 1
        Else
            If plusHautniveau < niveau Then
                plusHautniveau = niveau
            End If
        End If
        ' on replace dans le fichier
        FileOpen(1, "stats.ini", OpenMode.Output)
        Print(1, nbrParGagnées & vbCrLf)
        Print(1, nbrParPerdues & vbCrLf)
        Print(1, plusHautniveau & vbCrLf)
        Print(1, nbrJeuTerminé & vbCrLf)
        FileClose(1)
    End Sub

    'quans on ferme la fenetre on reinitilise l'accueil pour les stats, parties etc...
    Private Sub Fermeture() Handles Me.FormClosed
        accueil.Initialisation()
    End Sub
End Class

