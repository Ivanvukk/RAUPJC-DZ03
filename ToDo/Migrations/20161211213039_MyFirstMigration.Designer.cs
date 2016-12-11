﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ToDo.Models;

namespace ToDo.Migrations
{
    [DbContext(typeof(ToDoContext))]
    [Migration("20161211213039_MyFirstMigration")]
    partial class MyFirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ToDo.Models.TodoItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateCompleted");

                    b.Property<DateTime>("DateCreated");

                    b.Property<bool>("IsCompleted");

                    b.Property<string>("Text");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("TodoItem");
                });
        }
    }
}
