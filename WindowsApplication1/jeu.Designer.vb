<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class jeu
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(jeu))
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextRestant = New System.Windows.Forms.Label
        Me.TextResultat = New System.Windows.Forms.Label
        Me.TextNiveau = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.TextLigne = New System.Windows.Forms.NumericUpDown
        Me.TextColonne = New System.Windows.Forms.NumericUpDown
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        CType(Me.TextLigne, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextColonne, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(501, 182)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(56, 19)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Tir"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(578, 211)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Tirs restants :"
        '
        'TextRestant
        '
        Me.TextRestant.AutoSize = True
        Me.TextRestant.BackColor = System.Drawing.Color.Transparent
        Me.TextRestant.Location = New System.Drawing.Point(655, 211)
        Me.TextRestant.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.TextRestant.Name = "TextRestant"
        Me.TextRestant.Size = New System.Drawing.Size(0, 13)
        Me.TextRestant.TabIndex = 4
        '
        'TextResultat
        '
        Me.TextResultat.AutoSize = True
        Me.TextResultat.BackColor = System.Drawing.Color.Transparent
        Me.TextResultat.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextResultat.Location = New System.Drawing.Point(390, 247)
        Me.TextResultat.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.TextResultat.Name = "TextResultat"
        Me.TextResultat.Size = New System.Drawing.Size(0, 24)
        Me.TextResultat.TabIndex = 5
        '
        'TextNiveau
        '
        Me.TextNiveau.AutoSize = True
        Me.TextNiveau.BackColor = System.Drawing.Color.Transparent
        Me.TextNiveau.Location = New System.Drawing.Point(373, 79)
        Me.TextNiveau.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.TextNiveau.Name = "TextNiveau"
        Me.TextNiveau.Size = New System.Drawing.Size(39, 13)
        Me.TextNiveau.TabIndex = 6
        Me.TextNiveau.Text = "Label2"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(384, 106)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Ligne :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(384, 142)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Colonne :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(638, 93)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(0, 13)
        Me.Label4.TabIndex = 9
        '
        'TextLigne
        '
        Me.TextLigne.Location = New System.Drawing.Point(440, 106)
        Me.TextLigne.Margin = New System.Windows.Forms.Padding(2)
        Me.TextLigne.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.TextLigne.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.TextLigne.Name = "TextLigne"
        Me.TextLigne.Size = New System.Drawing.Size(50, 20)
        Me.TextLigne.TabIndex = 10
        Me.TextLigne.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'TextColonne
        '
        Me.TextColonne.Location = New System.Drawing.Point(440, 142)
        Me.TextColonne.Margin = New System.Windows.Forms.Padding(2)
        Me.TextColonne.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.TextColonne.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.TextColonne.Name = "TextColonne"
        Me.TextColonne.Size = New System.Drawing.Size(50, 20)
        Me.TextColonne.TabIndex = 11
        Me.TextColonne.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(371, 307)
        Me.Button2.Margin = New System.Windows.Forms.Padding(2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(139, 19)
        Me.Button2.TabIndex = 12
        Me.Button2.Text = "Sauvegarder la partie"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(526, 304)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(122, 23)
        Me.Button3.TabIndex = 13
        Me.Button3.Text = "Niveau suivant"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'jeu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(659, 336)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.TextColonne)
        Me.Controls.Add(Me.TextLigne)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextNiveau)
        Me.Controls.Add(Me.TextResultat)
        Me.Controls.Add(Me.TextRestant)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "jeu"
        Me.Text = "Bataille Navale - Jeu"
        CType(Me.TextLigne, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextColonne, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextRestant As System.Windows.Forms.Label
    Friend WithEvents TextResultat As System.Windows.Forms.Label
    Friend WithEvents TextNiveau As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextLigne As System.Windows.Forms.NumericUpDown
    Friend WithEvents TextColonne As System.Windows.Forms.NumericUpDown
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button

End Class
