import { Module } from '@nestjs/common';
import { DatabaseModule } from "../../database/database.module";
import { CommentService } from "./comment.service";
import { CommentRepository } from "./comment.repository";
import { CommentEntity } from "../../database/entities/comment.entity";
import { CommentController } from "./comment.controller";
import {MapModule} from "../map/map.module";


@Module({
  imports: [
    DatabaseModule,
    MapModule
  ],
  providers: [
    CommentService,
    CommentRepository,
    {
      provide: 'COMMENTS_REPOSITORY',
      useValue: CommentEntity,
    },
  ],
  controllers: [ CommentController ],
  exports: [ CommentService ],
})
export class CommentModule {}
