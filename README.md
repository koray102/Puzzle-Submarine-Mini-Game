# 🎮 Puzzle-Submarine-Mini-Game - Unity Mini-Game Collection

## [ENG]
> A scalable Unity game project gathering diverse game mechanics and technical architectures under a single roof.

## 📋 Project Summary
This project features two distinct mini-games with unique mechanics and technical infrastructures, accessible via a **Main Menu (Hub)**. The objective is to implement various disciplines (Physics-based control, UI-based logic, Data management) within the Unity engine, adhering to clean code principles.

## 🕹️ Mini Games

### 1. Jigsaw Puzzle (Logic & UI)
A version of the classic jigsaw mechanic enhanced with "Game Feel" elements.
* **Mechanic:** Pieces are moved via Drag & Drop and automatically snap into place when correctly positioned.
* **Key Features:**
    * **Hover Effects:** Smooth scale up/down effects using `Lerp` when hovering over pieces with the mouse.
    * **Procedural Celebration:** When the puzzle is completed, pieces vibrate organically using `Sinus` (Sine wave) to create a celebration effect.

### 2. Submarine Explorer & Quiz (Physics & Exploration)
A hybrid combination of a physics-based exploration game and a quiz mechanic.
* **Mechanic:** The player controls a physics-based submarine, collects chests in the ocean, and completes the game by answering questions at the end of the level.
* **Key Features:**
    * **Jitter-Free Physics:** Physics (FixedUpdate) and visual (Update) calculations were decoupled for submarine movement, ensuring butter-smooth rendering (e.g., 144Hz) even with a 50 FPS physics engine.
    * **Idle Animations:** Code-based procedural bobbing animations using `Mathf.Sin` while the submarine is stationary.
    * **Quiz System:** Level-end questions and answers are modularized using the `ScriptableObject` architecture.

---

## 🛠️ Technical Architecture & Design Patterns
Spaghetti code was avoided during development; industry-standard **SOLID** principles and **Design Patterns** were utilized.

### 🏗️ Patterns Used
* **Singleton Pattern:** Used for the `GameManager` controlling the general game flow and scene transitions (via Lazy Instantiation).
* **Interface (IInteractable):** Chests and other interactable objects in the submarine game implement the `IInteractable` interface. This allows the player code to call `OnInteract()` without needing to know the specific type of the object.
* **ScriptableObjects:** Quiz questions, options, and correct answers are stored as designer-friendly `ScriptableObject` data files instead of being hardcoded.

### 💻 Technical Details
* **Input System:** Keyboard and Mouse inputs.
* **Tweening:** Custom animation functions were written using mathematical formulas (Lerp, Sin) instead of using ready-made assets.
* **Scene Management:** Transitions between mini-games are handled via scene loading.

## 🎮 Controls
* **Menu:** Selection via Mouse.
* **Submarine:** `WASD` or `Arrow Keys` for movement, `E` for interaction (Opening chests).
* **Puzzle:** Drag & Drop via Mouse.

## 🚀 Installation
1. Clone the project.
2. Open the project with Unity Hub.
3. Start the `Scenes/MainMenu` scene.

---
*Developer: Doruk Koray Kocoglu*

  
## [TR]
> Farklı oyun mekaniklerinin ve teknik mimarilerin tek bir çatı altında toplandığı, genişletilebilir bir Unity oyun projesi.

## 📋 Proje Özeti
Bu proje, oyuncunun **Ana Menü (Hub)** üzerinden erişebildiği, birbirinden farklı mekaniklere ve teknik altyapıya sahip iki ana mini oyunu içerir. Amaç, Unity motoru üzerinde farklı disiplinleri (Fizik tabanlı kontrol, UI tabanlı mantık, Veri yönetimi) temiz kod prensipleriyle uygulamaktır.

## 🕹️ Mini Oyunlar

### 1. Jigsaw Puzzle (Mantık & UI)
Klasik yapboz mekaniğinin "Game Feel" (Oyun Hissi) öğeleriyle güçlendirilmiş versiyonudur.
* **Mekanik:** Parçalar sürükle-bırak (Drag & Drop) yöntemiyle taşınır ve doğru yere gelince otomatik kilitlenir.
* **Öne Çıkan Özellikler:**
    * **Hover Efektleri:** Mouse ile parça üzerine gelindiğinde `Lerp` kullanılarak yumuşak büyüme/küçülme efektleri.
    * **Procedural Celebration:** Puzzle tamamlandığında parçalar `Sinus` kullanılarak organik bir şekilde titreyerek kutlama efekti oluşturur.

### 2. Submarine Explorer & Quiz (Fizik & Keşif)
Fizik tabanlı bir keşif oyunu ile bilgi yarışması mekaniğinin hibrit birleşimidir.
* **Mekanik:** Oyuncu fizik tabanlı bir denizaltıyı kontrol eder, okyanustaki sandıkları toplar ve bölüm sonunda çıkan soruları yanıtlayarak oyunu bitirir.
* **Öne Çıkan Özellikler:**
    * **Jitter-Free Physics:** Denizaltı hareketinde fizik (FixedUpdate) ve görsel (Update) hesaplamaları ayrılarak, 50 FPS fizik motorunda bile 144Hz pürüzsüz (Butter Smooth) görüntü sağlandı.
    * **Idle Animations:** Denizaltı dururken kod tabanlı (Mathf.Sin) prosedürel dalgalanma animasyonları yapar.
    * **Quiz Sistemi:** Bölüm sonu soruları ve cevapları `ScriptableObject` mimarisi ile modüler hale getirilmiştir.

---

## 🛠️ Teknik Mimari ve Tasarım Desenleri
Proje geliştirilirken spagetti koddan kaçınılmış, endüstri standardı **SOLID** prensipleri ve **Tasarım Desenleri** kullanılmıştır.

### 🏗️ Kullanılan Desenler (Patterns)
* **Singleton Pattern:** Oyunun genel akışını yöneten `GameManager` ve sahne geçişleri için kullanıldı. (Lazy Instantiation yöntemiyle).
* **Interface (IInteractable):** Denizaltı oyunundaki sandıklar ve diğer etkileşimli objeler `IInteractable` arayüzünü implemente eder. Bu sayede oyuncu kodu, karşısındaki objenin ne olduğunu bilmeden `OnInteract()` çağırabilir.
* **ScriptableObjects:** Quiz soruları, şıklar ve doğru cevaplar kodun içine gömülmek yerine, tasarımcı dostu `ScriptableObject` veri dosyaları olarak tutuldu.

### 💻 Teknik Detaylar
* **Input System:** Klavye ve Mouse girişleri.
* **Tweening:** Hazır asset kullanmak yerine, matematiksel formüller (Lerp, Sin) ile özel animasyon fonksiyonları yazıldı.
* **Scene Management:** Mini oyunlar arası geçişler sahne yükleme ile sağlandı.
  

## 🎮 Kontroller
* **Menü:** Mouse ile seçim.
* **Denizaltı:** `WASD` veya `Yön Tuşları` ile hareket, `E` ile etkileşim (Sandık açma).
* **Puzzle:** Mouse ile sürükle-bırak.

## 🚀 Kurulum
1. Projeyi klonlayın.
2. Unity Hub ile projeyi açın.
3. `Scenes/MainMenu` sahnesini başlatın.

---
*Geliştirici: Doruk Koray Kocoglu*
