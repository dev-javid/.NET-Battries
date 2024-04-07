-- Wont be executed as part of the dbup, execute manually in the logdb
-- public.hrms_logs definition
CREATE TABLE public.hrms_pro_logs (
	"Message" text NULL,
	"MessageTemplate" text NULL,
	"Level" int4 NULL,
	"Timestamp" timestamptz NULL,
	"Exception" text NULL,
	"LogEvent" jsonb NULL
);