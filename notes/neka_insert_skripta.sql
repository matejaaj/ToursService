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



INSERT INTO tours."Tours"
("Id", "Name", "Description", "ImageData", "Difficulty", "Tags", "Price", "Status", "AuthorId", "TotalLength", "StatusChangeTime", "Durations")
VALUES
(1001, 'Mountain Escape', 'A scenic hike through the mountain trails', 'image_base64_data', 2, '{hiking,nature,adventure}', '{"Amount": 45.0}', 1, 2000, '{"Unit": 0, "Length": 12.5}', '2025-09-16 12:00:00+00', '[{"Duration": "05:00:00", "TransportType": 2}]'),

(1002, 'River Cruise', 'Relaxing boat tour along the river with local guide', 'image_base64_data', 1, '{boat,relax,scenic}', '{"Amount": 30.0}', 1, 2000, '{"Unit": 0, "Length": 8.0}', '2025-09-16 12:05:00+00', '[{"Duration": "03:30:00", "TransportType": 3}]'),

(1003, 'Countryside Cycling', 'Cycling adventure through villages and countryside paths', 'image_base64_data', 3, '{cycling,fitness,nature}', '{"Amount": 25.0}', 1, 2000, '{"Unit": 0, "Length": 20.0}', '2025-09-16 12:10:00+00', '[{"Duration": "04:00:00", "TransportType": 1}]');


INSERT INTO tours."Checkpoints"
("Id", "TourId", "Name", "Description", "ImageData", "Location", "Secret", "EncounterIds", "IsPublic")
VALUES
-- Checkpoints za Mountain Escape (TourId = 1001)
(1004, 1001, 'Trail Start', 'Beginning of the mountain hike', 'img_trail', '{"City": "Zlatibor", "Country": "Serbia", "Latitude": 43.72, "Longitude": 19.70}', 'secret101', '{}', true),
(1005, 1001, 'Waterfall View', 'Scenic stop near the waterfall', 'img_waterfall', '{"City": "Zlatibor", "Country": "Serbia", "Latitude": 43.74, "Longitude": 19.72}', 'secret102', '{}', true),
(1006, 1001, 'Mountain Peak', 'Highest point of the tour', 'img_peak', '{"City": "Zlatibor", "Country": "Serbia", "Latitude": 43.76, "Longitude": 19.74}', 'secret103', '{}', true),

-- Checkpoints za River Cruise (TourId = 1002)
(1007, 1002, 'Dock', 'Starting point of the river cruise', 'img_dock', '{"City": "Belgrade", "Country": "Serbia", "Latitude": 44.82, "Longitude": 20.45}', 'secret201', '{}', true),
(1008, 1002, 'Bridge View', 'Beautiful view of the main bridge', 'img_bridge', '{"City": "Belgrade", "Country": "Serbia", "Latitude": 44.81, "Longitude": 20.43}', 'secret202', '{}', true),
(1009, 1002, 'Island Stop', 'Small island with a beach', 'img_island', '{"City": "Belgrade", "Country": "Serbia", "Latitude": 44.80, "Longitude": 20.41}', 'secret203', '{}', true),

-- Checkpoints za Countryside Cycling (TourId = 1003)
(1010, 1003, 'Village Center', 'Meeting point in the countryside village', 'img_village', '{"City": "Sremski Karlovci", "Country": "Serbia", "Latitude": 45.10, "Longitude": 20.05}', 'secret301', '{}', true),
(1011, 1003, 'Vineyard Path', 'Cycling through vineyards', 'img_vineyard', '{"City": "Sremski Karlovci", "Country": "Serbia", "Latitude": 45.11, "Longitude": 20.07}', 'secret302', '{}', true),
(1012, 1003, 'Riverbank Rest', 'Rest point by the Danube river', 'img_river', '{"City": "Sremski Karlovci", "Country": "Serbia", "Latitude": 45.12, "Longitude": 20.09}', 'secret303', '{}', true);



COMMIT;