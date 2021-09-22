package main

import (
	"context"
	"database/sql"
	"github.com/heroiclabs/nakama-common/runtime"
	json "github.com/json-iterator/go"
)

// InitModule All Go modules must have a InitModule function with this exact signature.
func InitModule(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, initializer runtime.Initializer) error {
	// Register the RPC function.
	logger.Info("HELLO WORLD XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX")
	if err := initializer.RegisterRpc("my_unique_id", SomeExample); err != nil {
		logger.Error("Unable to register: %v", err)
		return err
	}
	return nil
}
func SomeExample(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, payload string) (string, error) {
	meta := make(map[string]interface{})
	// Note below, json.Unmarshal can only take a pointer as second argument
	if err := json.Unmarshal([]byte(payload), &meta); err != nil {
		// Handle error
		return "", err
	}

	id := "SomeId"
	authoritative := false
	sort := "desc"
	operator := "best"
	reset := "0 0 * * 1"

	if err := nk.LeaderboardCreate(ctx, id, authoritative, sort, operator, reset, meta); err != nil {
		// Handle error
		return "", err
	}

	return "Success", nil
}
