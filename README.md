# Unity 6.3 iOS DisplayLink foreground resume POC

Unity 6000.3.1f1에서 iOS 앱이 background/foreground로 전환될 때 DisplayLink를 pause/unpause하는 기능이 추가되었다. 이 변경 이후 background에서 foreground로 복귀할 때 Unity 스레드가 재개되지 않는 현상을 검증하기 위한 POC다.

관련 Unity 릴리스 노트: 
> iOS: Added support to pause and unpause DisplayLink when the application moved to the background or foreground.

[Unity 6000.3.1f1](https://unity.com/kr/releases/editor/whats-new/6000.3.1f1#notes)

## 테스트 환경
* Unity 6000.3.24f1
* GAMEPOT_UNITY_SDK_v364_20260728

## 증상

https://github.com/user-attachments/assets/2c289dd0-6738-4205-b7c7-8de4905738fb

iOS 앱을 background로 보냈다가 foreground로 돌아오면 Unity 스레드만 어떠한 동작도 하지 않는 상태가 된다. 게임 로직, 프레임 갱신, 입력 처리가 모두 중단되어 앱을 정상적으로 사용할 수 없다.

디버거에서 pause를 걸고, LLDB에서 아래 명령을 실행한 뒤 resume 하면 즉시 `Update`와 터치가 다시 동작한다.

```lldb
expr -l objc++ -- (void)[GetAppController() unpauseDisplayLink]
```

## 원인

Unity 6000.3.1f1부터 앱 lifecycle에 맞춰 DisplayLink를 자동으로 pause/unpause한다.

```text
sceneDidEnterBackground
  -> applicationDidEnterBackground
  -> UnityAppController -applicationDidEnterBackground:
  -> pauseDisplayLink

sceneWillEnterForeground
  -> GamePotAppDelegate -applicationWillEnterForeground:
  -> UnityAppController -applicationWillEnterForeground:
  -> unpauseDisplayLink
```

`GamePotAppDelegate`가 foreground lifecycle을 Unity의 `UnityAppController`까지 전달하지 않으면 background 진입 시 실행된 `pauseDisplayLink`만 남고 `unpauseDisplayLink`가 실행되지 않는다. 그 결과 iOS 앱 자체는 foreground 상태가 되어도 Unity 스레드는 계속 멈춰 있다.

## 해결

플러그인의 `applicationWillEnterForeground:` 구현에서 Unity app controller의 부모 lifecycle 메서드가 호출되도록 해야 한다.

```objc
[super applicationWillEnterForeground:application];
```

이 호출이 포함된 빌드에서는 Unity의 foreground 처리 과정에서 DisplayLink가 재개된다.

## 확인

1. foreground lifecycle이 Unity app controller까지 전달되도록 빌드한다.
2. 앱을 background로 전환한 뒤 다시 foreground로 전환한다.
3. Unity 스레드가 재개되어 게임 로직, 프레임 갱신, 입력 처리가 정상적으로 동작하는지 확인한다.
4. Xcode breakpoint에서 background 전환 시 `pauseDisplayLink`, foreground 전환 시 `unpauseDisplayLink`가 각각 호출되는지 확인한다.

```lldb
br s -n "-[UnityAppController unpauseDisplayLink]"
br s -n "-[UnityAppController pauseDisplayLink]"
```
