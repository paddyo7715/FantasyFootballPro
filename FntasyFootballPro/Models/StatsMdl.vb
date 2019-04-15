Public Class StatsMdl
    Property Game_ID As Integer = 0
    Property Player_ID As Integer = 0

    Property Started As Integer = 0
    Property Game_Played As Integer = 0
    Property Fumbles As Integer = 0

    'Passing
    Property Pass_Comp As Integer = 0
    Property Pass_Att As Integer = 0
    Property Pass_Yards As Integer = 0
    Property Pass_TDs As Integer = 0
    Property Pass_Ints As Integer = 0
    Property Pass_Sacks As Integer = 0

    'Offensive
    Property OLine_Sacks_Allowed As Integer = 0
    Property OLine_Rushing_Loss_Allowed As Integer = 0
    Property OLine_Pancakes As Integer = 0
    Property Rush_Att As Integer = 0
    Property Rush_Yards As Integer = 0
    Property Rush_TDs As Integer = 0
    Property Rec_Catches As Integer = 0
    Property Rec_Drops As Integer = 0
    Property Rec_Yards As Integer = 0
    Property Rec_TDs As Integer = 0

    'Defense
    Property Def_Sacks As Integer = 0
    Property Def_Rushing_Loss As Integer = 0
    Property Def_Tackles As Integer = 0
    Property Def_Missed_Tackles As Integer = 0
    Property Def_Ints As Integer = 0
    Property Pass_Defenses As Integer = 0
    Property Def_Interception_Yards As Integer = 0
    Property Def_Safeties As Integer = 0
    Property Def_TDs As Integer = 0

    'Kicking Game
    Property num_punts As Integer = 0
    Property Punt_yards As Integer = 0
    Property Punt_killed_num As Integer = 0
    Property FG_Att As Integer = 0
    Property FG_Made As Integer = 0
    Property XP_Att As Integer = 0
    Property XP_Made As Integer = 0

    Public Sub New(ByVal Game_ID As Integer, ByVal Player_ID As Integer)
        Me.Game_ID = Game_ID
        Me.Player_ID = Player_ID
    End Sub
    Public Sub setQBStats(ByVal Started As Integer, ByVal Game_Played As Integer, ByVal Fumbles As Integer, ByVal Pass_Comp As Integer,
                          ByVal Pass_Att As Integer, ByVal Pass_Yards As Integer, ByVal Pass_TDs As Integer,
                          ByVal Pass_Ints As Integer, ByVal Pass_Sacks As Integer)
        Me.Started += Started
        Me.Game_Played += Game_Played
        Me.Fumbles += Fumbles
        Me.Pass_Comp += Pass_Comp
        Me.Pass_Att += Pass_Att
        Me.Pass_Yards += Pass_Yards
        Me.Pass_TDs += Pass_TDs
        Me.Pass_Ints += Pass_Ints
        Me.Pass_Sacks += Pass_Sacks

    End Sub

    Public Sub setRBStats(ByVal Started As Integer, ByVal Game_Played As Integer, ByVal Fumbles As Integer, ByVal Rush_Att As Integer,
                          ByVal Rush_Yards As Integer, ByVal Rush_TDs As Integer, ByVal Rec_Catches As Integer,
                          ByVal Rec_Drops As Integer, ByVal Rec_Yards As Integer, ByVal Rec_TDs As Integer)

        Me.Started += Started
        Me.Game_Played += Game_Played
        Me.Fumbles += Fumbles
        Me.Rush_Att += Rush_Att
        Me.Rush_Yards += Rush_Yards
        Me.Rush_TDs += Rush_TDs
        Me.Rec_Catches += Rec_Catches
        Me.Rec_Drops += Rec_Drops
        Me.Rec_Yards += Rec_Yards
        Me.Rec_TDs += Rec_TDs

    End Sub

    Public Sub setWRStats(ByVal Started As Integer, ByVal Game_Played As Integer, ByVal Fumbles As Integer, ByVal Rec_Catches As Integer,
                      ByVal Rec_Drops As Integer, ByVal Rec_Yards As Integer, ByVal Rec_TDs As Integer)

        Me.Started += Started
        Me.Game_Played += Game_Played
        Me.Fumbles += Fumbles
        Me.Rec_Catches += Rec_Catches
        Me.Rec_Drops += Rec_Drops
        Me.Rec_Yards += Rec_Yards
        Me.Rec_TDs += Rec_TDs

    End Sub
    Public Sub setTEStats(ByVal Started As Integer, ByVal Game_Played As Integer, ByVal Fumbles As Integer, ByVal Rec_Catches As Integer,
                          ByVal Rec_Drops As Integer, ByVal Rec_Yards As Integer, ByVal Rec_TDs As Integer, ByVal OLine_Sacks_Allowed As Integer,
                          ByVal OLine_Rushing_Loss_Allowed As Integer, ByVal OLine_Pancakes As Integer)


        Me.Started += Started
        Me.Game_Played += Game_Played
        Me.Fumbles += Fumbles
        Me.Rec_Catches += Rec_Catches
        Me.Rec_Drops += Rec_Drops
        Me.Rec_Yards += Rec_Yards
        Me.Rec_TDs += Rec_TDs
        Me.OLine_Sacks_Allowed += OLine_Sacks_Allowed
        Me.OLine_Rushing_Loss_Allowed += OLine_Rushing_Loss_Allowed
        Me.OLine_Sacks_Allowed += OLine_Sacks_Allowed

    End Sub

    Public Sub setOLStats(ByVal Started As Integer, ByVal Game_Played As Integer, ByVal OLine_Sacks_Allowed As Integer, ByVal OLine_Rushing_Loss_Allowed As Integer,
                          ByVal OLine_Pancakes As Integer)

        Me.Started += Started
        Me.Game_Played += Game_Played
        Me.OLine_Sacks_Allowed += OLine_Sacks_Allowed
        Me.OLine_Rushing_Loss_Allowed += OLine_Rushing_Loss_Allowed
        Me.OLine_Sacks_Allowed += OLine_Sacks_Allowed

    End Sub

    Public Sub setDLStats(ByVal Started As Integer, ByVal Game_Played As Integer, ByVal Def_Sacks As Integer, ByVal Def_Rushing_Loss As Integer,
                      ByVal Def_Tackles As Integer, ByVal Def_Missed_Tackles As Integer, ByVal Def_Safeties As Integer,
                          ByVal Def_TDs As Integer)

        Me.Started += Started
        Me.Game_Played += Game_Played
        Me.Def_Sacks += Def_Sacks
        Me.Def_Rushing_Loss += Def_Rushing_Loss
        Me.Def_Tackles += Def_Tackles
        Me.Def_Missed_Tackles += Def_Missed_Tackles
        Me.Def_Safeties = Def_Safeties
        Me.Def_TDs = Def_TDs

    End Sub

    Public Sub setLBStats(ByVal Started As Integer, ByVal Game_Played As Integer, ByVal Def_Sacks As Integer, ByVal Def_Rushing_Loss As Integer,
                  ByVal Def_Tackles As Integer, ByVal Def_Missed_Tackles As Integer, ByVal Def_Safeties As Integer,
                      ByVal Def_TDs As Integer, ByVal Def_Ints As Integer, ByVal Pass_Defenses As Integer,
                          ByVal Def_Interception_Yards As Integer)

        Me.Started += Started
        Me.Game_Played += Game_Played
        Me.Def_Sacks += Def_Sacks
        Me.Def_Rushing_Loss += Def_Rushing_Loss
        Me.Def_Tackles += Def_Tackles
        Me.Def_Missed_Tackles += Def_Missed_Tackles
        Me.Def_Safeties = Def_Safeties
        Me.Def_TDs = Def_TDs
        Me.Def_Ints = Def_Ints
        Me.Pass_Defenses = Pass_Defenses
        Me.Def_Interception_Yards = Def_Interception_Yards

    End Sub

    Public Sub setDBStats(ByVal Started As Integer, ByVal Game_Played As Integer,
              ByVal Def_Tackles As Integer, ByVal Def_Missed_Tackles As Integer,
                  ByVal Def_TDs As Integer, ByVal Def_Ints As Integer, ByVal Pass_Defenses As Integer,
                      ByVal Def_Interception_Yards As Integer)

        Me.Started += Started
        Me.Game_Played += Game_Played
        Me.Def_Tackles += Def_Tackles
        Me.Def_Missed_Tackles += Def_Missed_Tackles
        Me.Def_TDs = Def_TDs
        Me.Def_Ints = Def_Ints
        Me.Pass_Defenses = Pass_Defenses
        Me.Def_Interception_Yards = Def_Interception_Yards

    End Sub

    Public Sub setPStats(ByVal Started As Integer, ByVal Game_Played As Integer,
                         ByVal num_punts As Integer, ByVal punt_yards As Integer,
                         ByVal punt_killed_num As Integer)

        Me.Started += Started
        Me.Game_Played += Game_Played
        Me.num_punts = num_punts
        Me.Punt_yards = punt_yards
        Me.Punt_killed_num = punt_killed_num

    End Sub

    Public Sub setKStats(ByVal Started As Integer, ByVal Game_Played As Integer,
                     ByVal FG_Att As Integer, ByVal FG_Made As Integer,
                     ByVal XP_Att As Integer, ByVal XP_Made As Integer)

        Me.Started += Started
        Me.Game_Played += Game_Played
        Me.FG_Att = FG_Att
        Me.FG_Made = FG_Made
        Me.XP_Att = XP_Att
        Me.XP_Made = XP_Made

    End Sub


End Class
