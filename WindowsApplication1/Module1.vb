Module Module1
    'Le compte sélectionné
    Public chemin As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" + "bn\"
    Public utilisateur As String
    Public exPartie As Integer = 0
    Public nomExPartie As String

    'Niveau de difficulté lié au nombre de tirs possible
    Public niveau As Integer = 1
    Public niveau_max As Integer = 10
    Public Const coupsEnMoinsParNiveau As Integer = 6
    Public Const NbTirNiv1 As Integer = 86

End Module
