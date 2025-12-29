# ResultTypeFactory.cs Implementation Details

**File Path:** `Meta/Utils.Metalama.Factories/Factories/ResultTypeFactory.cs`
**Namespace:** `LightningArc.Utils.Metalama`

## Overview
Similar to `TaskTypeFactory`, but specialized for the `Result` and `Result<T>` types defined in `Utils.Results`.

## Code Analysis

### `GetType`
Generates references to `Result` or `Result<T>`.

### `GetTaskType`
Combines `TaskTypeFactory` and `ResultTypeFactory` to generate references to `Task<Result>` or `Task<Result<T>>`. This is extremely useful for aspects that need to wrap methods returning domain Results in async Tasks.
