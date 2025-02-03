# Ćwiczenia 4

Stwórz aplikacje REST API, która pozwoli nam zarządzać danymi zwierząt w bazie danych schroniska dla kliniki weterynaryjnej.

Zwierze opisywane jest przez:

- id
- imię
- kategoria (np. pies, kot)
- masa
- kolor sierści

Chcielibyśmy mieć możliwość:

1. pobierania listy zwierząt
2. pobierania danych konkretnego zwierzęcia po id
3. dodawania zwierzęcia
4. edycji danych zwierzęcia
5. usuwania zwierzęcia

Ponadto chcielibyśmy zapisywać informacje na temat wizyt zwierzęcia:

1. chcielibyśmy mieć możliwość pobrania listy wizyt powiązanych z danym zwierzęciem
2. chcielibyśmy mieć możliwość dodawania nowych wizyt

Wizyta obejmuje następujące informacje:

- data wizyty
- zwierzę
- opis wizyty
- cena wizyty

1. Przygotuj aplikację z REST API z odpowiednimi końcówkami HTTP - GET, POST, PUT, DELETE.
2. Upewnij się, że struktura końcówek jest zgodna z zasadami projektowania końcówek REST.
3. Jako bazą danych przygotuj statyczną kolekcję z obiektami.
4. Możesz wykorzystać zarówno podejście MinimalAPI lub skorzystać z wersji API wykorzystującej klasy kontrolerów.
5. Przetestuj przygotowaną aplikację z pomocą Postman'a.

## Komentarz
Zrefactorowałem sampleAPI i SampleAPIControllers zarządzające danymi studentów na takie które zarządza zwierzętami
oraz dodałem wymagane możliwości związane z wizytami.
Niestety brak jest jakiejkolwiek walidacji np. czy zwierzę o danym id już istnieje w bazie, itp ;)

