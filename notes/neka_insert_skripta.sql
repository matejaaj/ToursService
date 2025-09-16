BEGIN;

-- 1) OBRIŠI POSTOJEĆE PODATKE (redosled zbog FK-ova)
DELETE FROM tours."TourExecutions";
DELETE FROM tours."Checkpoints";
DELETE FROM tours."Tours";

-- ================== TOUR ==================
INSERT INTO tours."Tours"(
    "Id", "Name", "Description", "ImageData", "Difficulty", "Tags", "Price", "Status",
    "AuthorId", "TotalLength", "StatusChangeTime", "Durations")
VALUES
    (1000, 'City Adventure', 'A walking adventure through the old town', 'image_base64_data', 0,
     ARRAY['history','walking'],
     '{"Amount": 20.0}',
     1,
     2000,
     '{"Length": 5.0, "Unit": 0}',
     NOW(),
     '[{"Duration":"02:00:00","TransportType":2}]');

-- ================== CHECKPOINTS (Location sa City + Country) ==================
INSERT INTO tours."Checkpoints"(
    "Id", "TourId", "Name", "Description", "ImageData", "Location", "Secret", "EncounterIds", "IsPublic")
VALUES
    (1001, 1000, 'Main Square', 'Start of the tour', 'img_sq',
     '{"Latitude":45.25,"Longitude":19.85,"Country":"Serbia","City":"Novi Sad"}',
     'secret1', '{}', true),

    (1002, 1000, 'Old Castle', 'Historical stop', 'img_castle',
     '{"Latitude":45.26,"Longitude":19.86,"Country":"Serbia","City":"Novi Sad"}',
     'secret2', '{}', true),

    (1003, 1000, 'City Park', 'Relaxing finish', 'img_park',
     '{"Latitude":45.27,"Longitude":19.87,"Country":"Serbia","City":"Novi Sad"}',
     'secret3', '{}', true);

-- ================== TOUR EXECUTIONS ==================
-- Tourist 3000 finished the tour (all checkpoints completed)
INSERT INTO tours."TourExecutions"(
    "Id", "TourId", "TouristId", "Status", "LastActivity", "Completion", "Position", "CompletedCheckpoints")
VALUES
    (1004, 1000, 3000, 2, NOW(), 100.0,
     '{"Longitude":19.87,"Latitude":45.27}',
     '[{"CheckpointId":1001,"CompletionTime":"2025-08-01T10:00:00Z"},
       {"CheckpointId":1002,"CompletionTime":"2025-08-01T11:00:00Z"},
       {"CheckpointId":1003,"CompletionTime":"2025-08-01T12:00:00Z"}]');

-- Tourist 3001 started but did not complete anything
INSERT INTO tours."TourExecutions"(
    "Id", "TourId", "TouristId", "Status", "LastActivity", "Completion", "Position", "CompletedCheckpoints")
VALUES
    (1005, 1000, 3001, 0, NOW(), 0.0,
     '{"Longitude":19.85,"Latitude":45.25}', '[]');

COMMIT;
