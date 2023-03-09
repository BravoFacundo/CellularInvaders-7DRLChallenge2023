/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID PLAY_MUSIC = 2932040671U;
        static const AkUniqueID PLAY_TEST_BEEP_1KHZ = 4276556170U;
        static const AkUniqueID PLAY_TEST_BEEP_1KHZ_3D = 1816661476U;
        static const AkUniqueID PLAY_TEST_BEEP_500HZ = 676032487U;
        static const AkUniqueID PLAY_TEST_BEEP_500HZ_3D = 3602201827U;
        static const AkUniqueID PLAY_TEST_DRUMKIT_LOOP = 16532432U;
        static const AkUniqueID PLAY_TEST_DRUMKIT_LOOP_3D = 613478190U;
        static const AkUniqueID STOP_MUSIC = 2837384057U;
        static const AkUniqueID STOP_TEST_DRUMKIT_LOOP = 3484075926U;
        static const AkUniqueID STOP_TEST_DRUMKIT_LOOP_3D = 2503746936U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace GAMESTATUS
        {
            static const AkUniqueID GROUP = 1045871717U;

            namespace STATE
            {
                static const AkUniqueID INGAME = 984691642U;
                static const AkUniqueID INMENU = 3374585465U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace GAMESTATUS

        namespace POSITION
        {
            static const AkUniqueID GROUP = 1578750580U;

            namespace STATE
            {
                static const AkUniqueID ISCROSSING = 3620838037U;
                static const AkUniqueID ISINROOM = 2851713889U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace POSITION

        namespace ROOMTYPE
        {
            static const AkUniqueID GROUP = 1992536912U;

            namespace STATE
            {
                static const AkUniqueID INDANGEROUSROOM = 3385864285U;
                static const AkUniqueID INQUIETROOM = 3819053597U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace ROOMTYPE

    } // namespace STATES

    namespace SWITCHES
    {
        namespace PLAYERHEALTH
        {
            static const AkUniqueID GROUP = 151362964U;

            namespace SWITCH
            {
                static const AkUniqueID DEATH = 779278001U;
                static const AkUniqueID FULLHEALTH = 2429688720U;
                static const AkUniqueID LOWHEALTH = 1017222595U;
                static const AkUniqueID NEARDEATH = 898449699U;
            } // namespace SWITCH
        } // namespace PLAYERHEALTH

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID RTPC_DISTANCE = 262290038U;
        static const AkUniqueID RTPC_MASTERVOLUME = 2582501215U;
        static const AkUniqueID RTPC_MUSICVOLUME = 2378823330U;
        static const AkUniqueID RTPC_PLAYERLIFE = 4105377574U;
        static const AkUniqueID RTPC_SFXVOLUME = 2644490154U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAIN = 3161908922U;
        static const AkUniqueID TEST = 3157003241U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID AMBIENCEMASTER = 2611281843U;
        static const AkUniqueID FOESMASTER = 1836367610U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MUSICMASTER = 3199789248U;
        static const AkUniqueID PLAYERMASTER = 3538689948U;
        static const AkUniqueID SFXMASTER = 1681633080U;
        static const AkUniqueID TEST = 3157003241U;
    } // namespace BUSSES

    namespace AUX_BUSSES
    {
        static const AkUniqueID REVERBMASTER = 1686871543U;
    } // namespace AUX_BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
