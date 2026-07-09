# Why the EF Core CLI cannot use data annotations for everything

You’d think that if you run scaffolding with `--data-annotations`, EF Core would put as much as possible into attributes such as `[Key]`, `[MaxLength]`, `[Required]`, and so on, and skip the Fluent API entirely. 

But even with that flag, you’ll still see a generated `OnModelCreating` method full of fluent API calls.

The reason is that data annotations cover only a subset of EF Core’s configuration possibilities. For example:
- `[Required]` → `IsRequired()`
- `[MaxLength(50)]` → `HasMaxLength(50)`
- `[Column(TypeName = "decimal(18,2)")]` → `HasColumnType("decimal(18,2)")`

But many database features have no corresponding attribute. For example, composite keys, indexes, many-to-many join tables, default values, computed columns, and schemas beyond `dbo`. Since EF Core scaffolding must capture all aspects of the schema, it always falls back to Fluent API for things data annotations can’t represent.

The Fluent API is the authoritative superset that can configure everything. That’s why scaffolding generates it regardless of the flag, so you’ll always get a complete mapping. When you set the `--data-annotations` flag, EF Core puts things that can be expressed in attributes into the entity class itself, instead of leaving them *only* in the Fluent API.

When scaffolding, EF Core wants to produce a model that can be dropped into a project and round-tripped back to the same schema without loss. If it relied only on attributes, you’d lose fidelity when certain features can’t be annotated. By keeping the Fluent API, EF Core guarantees that the generated model is an exact representation of the database schema.

Data annotations live in your domain entity classes, which might feel intrusive if you think of them as plain POCOs. The Fluent API in `OnModelCreating` is non-intrusive and centralizes schema-related configuration. EF Core leans toward the Fluent API as the “master record,” even if annotations are also generated.
