Imports System.Drawing

Public Class App_Constants
    'Folders / Files
    Public Const LEAGUE_DB_FOLDER As String = "FFPro_Data"
    Public Const LEAGUE_HELMETS_SUBFOLDER As String = "Helmet_Images"
    Public Const LEAGUE_STADIUM_SUBFOLDER As String = "Stadium_Images"

    Public Const BLANK_DB_FOLDER As String = "/Database"
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
    Public Const SECONDARY_1_ABILITY_HIGH_RATING As Integer = 80

    Public Const SECONDARY_2_ABILITY_LOW_RATING As Integer = 20
    Public Const SECONDARY_2_ABILITY_HIGH_RATING As Integer = 70

    Public Const SECONDARY_3_ABILITY_LOW_RATING As Integer = 10
    Public Const SECONDARY_3_ABILITY_HIGH_RATING As Integer = 60

    Public Const TERTIARY_ABILITY_LOW_RATING As Integer = 1
    Public Const TERTIARY_ABILITY_HIGH_RATING As Integer = 10




End Class
