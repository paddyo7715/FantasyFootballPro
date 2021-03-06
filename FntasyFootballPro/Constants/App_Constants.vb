﻿Imports System.Drawing
Imports System.IO

Public Class App_Constants


    'Folders / Files
    Public Const MIN_FREE_DISK_SPACE As Integer = 50
    Public Const GAME_DOC_FOLDER As String = "Spect_Football_Data"
    Public Const LOG_FOLDER As String = "Logs"
    Public Const BACKUP_FOLDER As String = "Backup"
    Public Const LEAGUE_HELMETS_SUBFOLDER As String = "Helmet_Images"
    Public Const LEAGUE_STADIUM_SUBFOLDER As String = "Stadium_Images"

    'The following three were intended to be constants, but when I added the file seperator variable, they could no longer be Consts
    Public Shared APP_HELMET_FOLDER As String = Path.DirectorySeparatorChar & "Images" & Path.DirectorySeparatorChar & "Helmets" & Path.DirectorySeparatorChar
    Public Shared APP_STADIUM_FOLDER As String = Path.DirectorySeparatorChar & "Images" & Path.DirectorySeparatorChar & "Stadiums" & Path.DirectorySeparatorChar
    Public Shared BLANK_DB_FOLDER As String = Path.DirectorySeparatorChar & "Database"

    Public Const DB_FILE_EXT As String = "db"
    Public Const BLANK_DB As String = "BlankDB.db"

    'Players
    Public Const QB_PER_TEAM = 3
    Public Const RB_PER_TEAM = 4
    Public Const WR_PER_TEAM = 4
    Public Const TE_PER_TEAM = 2
    Public Const OL_PER_TEAM = 10

    Public Const DL_PER_TEAM = 8
    Public Const LB_PER_TEAM = 7
    Public Const DB_PER_TEAM = 8

    Public Const K_PER_TEAM = 1
    Public Const P_PER_TEAM = 1

    'Numbering
    Public Const QBLOWNUM As Integer = 7
    Public Const QBHIGHNUM As Integer = 18

    Public Const RBLOWNUM As Integer = 20
    Public Const RBHIGHNUM As Integer = 39

    Public Const OLLOWNUM As Integer = 60
    Public Const OLHIGHNUM As Integer = 79

    Public Const WRLOWNUM As Integer = 80
    Public Const WRHIGHNUM As Integer = 89

    Public Const TELOWNUM As Integer = 40
    Public Const TEHIGHNUM As Integer = 89

    Public Const DLLOWNUM As Integer = 70
    Public Const DLHIGHNUM As Integer = 99

    Public Const LBLOWNUM As Integer = 50
    Public Const LBHIGHNUM As Integer = 59

    Public Const DBLOWNUM As Integer = 20
    Public Const DBHIGHNUM As Integer = 49

    Public Const KLOWNUM As Integer = 1
    Public Const KHIGHNUM As Integer = 6

    'Player Ratings Percentages
    Public Const QB_FUMBLE_PERCENT = 0.03
    Public Const QB_ARMSTRENGTH_PERCENT = 0.15
    Public Const QB_ACCURACY_RATING = 0.45
    Public Const QB_DESISION_RATING = 0.37

    Public Const RB_RUNNING_PWER_PERCENT = 0.28
    Public Const RB_SPEED_PERCENT = 0.2
    Public Const RB_AGILITY_PERCENT = 0.28
    Public Const RB_FUMBLE_PERCENT = 0.06
    Public Const RB_HANDS_PERCENT = 0.18

    Public Const WR_SPEED_PERCENT = 0.35
    Public Const WR_AGILITY_PERCENT = 0.35
    Public Const WR_FUMBLE_PERCENT = 0.06
    Public Const WR_HANDS_PERCENT = 0.24

    Public Const TE_SPEED_PERCENT = 0.2
    Public Const TE_AGILITY_PERCENT = 0.3
    Public Const TE_FUMBLE_PERCENT = 0.03
    Public Const TE_HANDS_PERCENT = 0.3
    Public Const TE_PASS_BLOCK_PERCENT = 0.04
    Public Const TE_RUN_BLOCK_PERCENT = 0.13

    Public Const OL_PASS_BLOCK_PERCENT = 0.28
    Public Const OL_RUN_BLOCK_PERCENT = 0.44
    Public Const OL_AGILITY_PERCENT = 0.28

    Public Const DL_PASS_ATTACK_PERCENT = 0.2
    Public Const DL_RUN_ATTACK_PERCENT = 0.33
    Public Const DL_TACKLE_PERCENT = 0.2
    Public Const DL_AGILITY_PERCENT = 0.2
    Public Const DL_SPEED_PERCENT = 0.07

    Public Const DB_SPEED_PERCENT = 0.28
    Public Const DB_HANDS_PERCENT = 0.16
    Public Const DB_TACKLING_PERCENT = 0.28
    Public Const DB_AGILITY_PERCENT = 0.28

    Public Const LB_PASS_ATTACK_PERCENT = 0.3
    Public Const LB_RUN_ATTACK_PERCENT = 0.2
    Public Const LB_TACKLE_PERCENT = 0.2
    Public Const LB_HANDS_PERCENT = 0.06
    Public Const LB_SPEED_PERCENT = 0.12
    Public Const LB_AGILITY_PERCENT = 0.12

    Public Const K_KICK_ACC = 0.65
    Public Const K_LEG_STRENGTH = 0.35


    'Stock Uniform Colors
    Public Shared STOCK_GREY_COLOR As Color = Color.FromArgb(40, 40, 40)

    Public Shared STOCK_HELMET_COLOR As Color = Color.FromArgb(150, 107, 117)
    Public Shared STOCK_FACEMASK As Color = Color.FromArgb(203, 19, 31)
    Public Shared STOCK_HEL_LOGO_COLOR As Color = Color.FromArgb(33, 56, 123)

    Public Shared STOCK_JERSEY_COLOR As Color = Color.FromArgb(128, 0, 0)
    Public Shared STOCK_NUMBER_COLOR As Color = Color.FromArgb(255, 255, 255)
    Public Shared STOCK_NUM_OUTLINE_COLOR As Color = Color.FromArgb(0, 16, 88)
    Public Shared STOCK_SLEEVE_COLOR As Color = Color.FromArgb(0, 255, 255)
    Public Shared STOCK_SHOULDER_STRIPE_COLOR As Color = Color.FromArgb(45, 84, 161)
    Public Shared STOCK_SLEEVE_STRIPE_1_COLOR As Color = Color.FromArgb(206, 40, 48)
    Public Shared STOCK_SLEEVE_STRIPE_2_COLOR As Color = Color.FromArgb(0, 128, 0)
    Public Shared STOCK_SLEEVE_STRIPE_3_COLOR As Color = Color.FromArgb(255, 255, 0)
    Public Shared STOCK_SLEEVE_STRIPE_4_COLOR As Color = Color.FromArgb(247, 52, 26)
    Public Shared STOCK_SLEEVE_STRIPE_5_COLOR As Color = Color.FromArgb(90, 4, 65)
    Public Shared STOCK_SLEEVE_STRIPE_6_COLOR As Color = Color.FromArgb(128, 54, 91)

    Public Shared STOCK_PANTS_COLOR As Color = Color.FromArgb(205, 82, 64)
    Public Shared STOCK_PANTS_STRIPE_1_COLOR As Color = Color.FromArgb(128, 128, 0)
    Public Shared STOCK_PANTS_STRIPE_2_COLOR As Color = Color.FromArgb(251, 91, 59)
    Public Shared STOCK_PANTS_STRIPE_3_COLOR As Color = Color.FromArgb(128, 128, 128)

    Public Shared STOCK_SOCKS_COLOR As Color = Color.FromArgb(236, 133, 102)
    Public Shared STOCK_CLEATES_COLOR As Color = Color.FromArgb(113, 89, 111)


    'age
    Public Const STARTING_ROOKIE_AGE As Integer = 20
    Public Const NEWLEAGE_LOW_AGE As Integer = 20
    Public Const NEWLEAGE_HIGH_AGE As Integer = 31

    'Abilities
    Public Const OL_RUN_PASS_BLOCK_DELTA As Integer = 5

    Public Const PRIMARY_ABILITY_LOW_RATING As Integer = 40
    Public Const PRIMARY_ABILITY_HIGH_RATING As Integer = 100

    Public Const SECONDARY_1_ABILITY_LOW_RATING As Integer = 30
    Public Const SECONDARY_1_ABILITY_HIGH_RATING As Integer = 90

    Public Const SECONDARY_2_ABILITY_LOW_RATING As Integer = 20
    Public Const SECONDARY_2_ABILITY_HIGH_RATING As Integer = 80

    Public Const SECONDARY_3_ABILITY_LOW_RATING As Integer = 10
    Public Const SECONDARY_3_ABILITY_HIGH_RATING As Integer = 70

    Public Const TERTIARY_ABILITY_LOW_RATING As Integer = 1
    Public Const TERTIARY_ABILITY_HIGH_RATING As Integer = 10

    'Teams
    Public Const EMPTY_TEAM_SLOT As String = "Empty Team Slot"


End Class
