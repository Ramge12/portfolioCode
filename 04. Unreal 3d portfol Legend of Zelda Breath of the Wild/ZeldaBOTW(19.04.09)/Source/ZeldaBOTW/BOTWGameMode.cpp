// Fill out your copyright notice in the Description page of Project Settings.

#include "BOTWGameMode.h"
#include "Player/PlayerCharacter.h"
#include "Player/Controller/PlayerCharacterController.h"
#include "Player/Controller/PlayerChracterMoveController.h"

ABOTWGameMode::ABOTWGameMode()
{
	DefaultPawnClass = APlayerCharacter::StaticClass();
	PlayerControllerClass = APlayerCharacterController::StaticClass();
}



