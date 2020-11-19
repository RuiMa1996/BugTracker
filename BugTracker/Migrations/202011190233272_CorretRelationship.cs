namespace BugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorretRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TicketPriorities", "Ticket_id", "dbo.Tickets");
            DropForeignKey("dbo.TicketStatus", "Ticket_id", "dbo.Tickets");
            DropForeignKey("dbo.TicketTypes", "Ticket_id", "dbo.Tickets");
            DropIndex("dbo.TicketPriorities", new[] { "Ticket_id" });
            DropIndex("dbo.TicketStatus", new[] { "Ticket_id" });
            DropIndex("dbo.TicketTypes", new[] { "Ticket_id" });
            AddColumn("dbo.TicketAttachments", "TicketId", c => c.Int(nullable: false));
            AddColumn("dbo.Tickets", "Project_Id", c => c.Int());
            AlterColumn("dbo.Tickets", "OwnerUserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Tickets", "AssignedToUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.TicketAttachments", "TicketId");
            CreateIndex("dbo.Tickets", "TicketTypeId");
            CreateIndex("dbo.Tickets", "TicketStatusId");
            CreateIndex("dbo.Tickets", "OwnerUserId");
            CreateIndex("dbo.Tickets", "AssignedToUserId");
            CreateIndex("dbo.Tickets", "Project_Id");
            CreateIndex("dbo.TicketComments", "TicketId");
            CreateIndex("dbo.TicketHistories", "TicketId");
            CreateIndex("dbo.TicketNotifications", "TicketId");
            AddForeignKey("dbo.Tickets", "AssignedToUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Tickets", "OwnerUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Tickets", "Project_Id", "dbo.Projects", "Id");
            AddForeignKey("dbo.Tickets", "TicketStatusId", "dbo.TicketStatus", "id", cascadeDelete: true);
            AddForeignKey("dbo.Tickets", "TicketTypeId", "dbo.TicketTypes", "id", cascadeDelete: true);
            AddForeignKey("dbo.TicketAttachments", "TicketId", "dbo.Tickets", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TicketComments", "TicketId", "dbo.Tickets", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TicketHistories", "TicketId", "dbo.Tickets", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TicketNotifications", "TicketId", "dbo.Tickets", "Id", cascadeDelete: true);
            DropColumn("dbo.TicketAttachments", "TicletId");
            DropColumn("dbo.TicketPriorities", "Ticket_id");
            DropColumn("dbo.TicketStatus", "Ticket_id");
            DropColumn("dbo.TicketTypes", "Ticket_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TicketTypes", "Ticket_id", c => c.Int());
            AddColumn("dbo.TicketStatus", "Ticket_id", c => c.Int());
            AddColumn("dbo.TicketPriorities", "Ticket_id", c => c.Int());
            AddColumn("dbo.TicketAttachments", "TicletId", c => c.Int(nullable: false));
            DropForeignKey("dbo.TicketNotifications", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.TicketHistories", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.TicketComments", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.TicketAttachments", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.Tickets", "TicketTypeId", "dbo.TicketTypes");
            DropForeignKey("dbo.Tickets", "TicketStatusId", "dbo.TicketStatus");
            DropForeignKey("dbo.Tickets", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.Tickets", "OwnerUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tickets", "AssignedToUserId", "dbo.AspNetUsers");
            DropIndex("dbo.TicketNotifications", new[] { "TicketId" });
            DropIndex("dbo.TicketHistories", new[] { "TicketId" });
            DropIndex("dbo.TicketComments", new[] { "TicketId" });
            DropIndex("dbo.Tickets", new[] { "Project_Id" });
            DropIndex("dbo.Tickets", new[] { "AssignedToUserId" });
            DropIndex("dbo.Tickets", new[] { "OwnerUserId" });
            DropIndex("dbo.Tickets", new[] { "TicketStatusId" });
            DropIndex("dbo.Tickets", new[] { "TicketTypeId" });
            DropIndex("dbo.TicketAttachments", new[] { "TicketId" });
            AlterColumn("dbo.Tickets", "AssignedToUserId", c => c.String());
            AlterColumn("dbo.Tickets", "OwnerUserId", c => c.String());
            DropColumn("dbo.Tickets", "Project_Id");
            DropColumn("dbo.TicketAttachments", "TicketId");
            CreateIndex("dbo.TicketTypes", "Ticket_id");
            CreateIndex("dbo.TicketStatus", "Ticket_id");
            CreateIndex("dbo.TicketPriorities", "Ticket_id");
            AddForeignKey("dbo.TicketTypes", "Ticket_id", "dbo.Tickets", "id");
            AddForeignKey("dbo.TicketStatus", "Ticket_id", "dbo.Tickets", "id");
            AddForeignKey("dbo.TicketPriorities", "Ticket_id", "dbo.Tickets", "id");
        }
    }
}
