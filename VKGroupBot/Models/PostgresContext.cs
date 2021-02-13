using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace VKGroupBot
{
    public partial class PostgresContext : DbContext
    {
        public PostgresContext()
        {
        }

        public PostgresContext(DbContextOptions<PostgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Couple> Couples { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Link> Links { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
	            string host = Environment.GetEnvironmentVariable("database_host");
	            string port = Environment.GetEnvironmentVariable("database_port");
	            string database = Environment.GetEnvironmentVariable("database_name");
	            string username = Environment.GetEnvironmentVariable("database_username");
	            string password = Environment.GetEnvironmentVariable("database_password");
                optionsBuilder.UseNpgsql($"Host={host};Port={port};Database={database};Username={username};Password={password}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "C");

            modelBuilder.Entity<Couple>(entity =>
            {
                entity.HasKey(e => e.CoupleUi)
                    .HasName("couples_pkey");

                entity.ToTable("couples");

                entity.Property(e => e.CoupleUi)
                    .HasMaxLength(100)
                    .HasColumnName("couple_ui");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.NameCoupleEven)
                    .HasMaxLength(100)
                    .HasColumnName("name_couple_even");

                entity.Property(e => e.NameCoupleOdd)
                    .HasMaxLength(100)
                    .HasColumnName("name_couple_odd");

                entity.Property(e => e.TimeBreak)
                    .HasMaxLength(20)
                    .HasColumnName("time_break");

                entity.Property(e => e.TimeEnd)
                    .HasMaxLength(20)
                    .HasColumnName("time_end");

                entity.Property(e => e.TimeStart)
                    .HasMaxLength(20)
                    .HasColumnName("time_start");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("events");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Date)
                    .HasMaxLength(100)
                    .HasColumnName("date");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.SubjectName)
                    .HasMaxLength(100)
                    .HasColumnName("subject_name");

                entity.Property(e => e.Time)
                    .HasMaxLength(100)
                    .HasColumnName("time");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<Link>(entity =>
            {
                entity.HasKey(e => e.SubjectName)
                    .HasName("links_pkey");

                entity.ToTable("links");

                entity.Property(e => e.SubjectName)
                    .HasMaxLength(100)
                    .HasColumnName("subject_name");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.LecturesCode)
                    .HasMaxLength(100)
                    .HasColumnName("lectures_code");

                entity.Property(e => e.LecturesLink)
                    .HasMaxLength(1000)
                    .HasColumnName("lectures_link");

                entity.Property(e => e.LecturesPassword)
                    .HasMaxLength(100)
                    .HasColumnName("lectures_password");

                entity.Property(e => e.PracticesCode)
                    .HasMaxLength(100)
                    .HasColumnName("practices_code");

                entity.Property(e => e.PracticesLink)
                    .HasMaxLength(1000)
                    .HasColumnName("practices_link");

                entity.Property(e => e.PracticesPassword)
                    .HasMaxLength(100)
                    .HasColumnName("practices_password");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("subjects");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.SubjectName)
                    .HasMaxLength(100)
                    .HasColumnName("subject_name");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("teachers");

                entity.Property(e => e.TeacherId).HasColumnName("teacher_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.KindOfActivity)
                    .HasMaxLength(100)
                    .HasColumnName("kind_of_activity");

                entity.Property(e => e.SubjectName)
                    .HasMaxLength(100)
                    .HasColumnName("subject_name");

                entity.Property(e => e.TeacherMail)
                    .HasMaxLength(100)
                    .HasColumnName("teacher_mail");

                entity.Property(e => e.TeacherName)
                    .HasMaxLength(100)
                    .HasColumnName("teacher_name");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Admin).HasColumnName("admin");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .HasColumnName("full_name");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Subscription).HasColumnName("subscription");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("now()");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
